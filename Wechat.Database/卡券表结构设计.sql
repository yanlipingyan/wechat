drop database if exists wechat;
create database wechat;

use wechat;

drop table if exists card;
create table card
(
	Id int auto_increment primary key comment '主键',
	Guid char(32) not null comment 'Guid',
	CardId varchar(50) null comment '卡券Id，在创建后由微信审核通过后填写',
	CardType varchar(24) not null comment '卡券类型：GROUPON(团购券)；CASH(代金券)；DISCOUNT(折扣券)；GIFT(礼品券)；GENERAL_COUPON(通用优惠券)；MEMBER_CARD(会员卡)；MEETING_TICKET(门票券)',
	LogoUrl varchar(128) not null comment '卡券的商户logo',
	CodeType varchar(16) not null comment 'Code展示类型：CODE_TYPE_TEXT(文本)；CODE_TYPE_BARCODE(一维码)；CODE_TYPE_QRCODE(二维码)；CODE_TYPE_ONLY_QRCODE(二维码无code显示)；CODE_TYPE_ONLY_BARCODE(一维码无code显示)',
	BrandName varchar(36) not null comment '商户名字,字数上限为12个汉字。',
	Title varchar(27) not null comment '卡券名，字数上限为9个汉字。(建议涵盖卡券属性、服务及金额)。',
	SubTitle varchar(54) default null comment '卡券副标题',
	Color varchar(16) not null comment '券颜色',
	Notice varchar(48) not null comment '卡券使用提醒，字数上限为16个汉字。',
	Description varchar(3072) not null comment '卡券使用说明，字数上限为1024个汉字。',
	
	Quantity int not null default 1 comment '卡券库存的数量，不支持填写0，上限为100000000。',
	
	`Type` int not null default 1 comment '使用时间的类型，旧文档采用的1和2依然生效。1为固定日期区间(DATE_TYPE_FIX_TIME_RANGE)，2为固定时长(DATE_TYPE_FIX_TERM)（自领取后按天算）',
	BeginTimestamp int unsigned not null default 0 comment 'type为1时专用，表示起用时间。从1970年1月1日00:00:00至起用时间的秒数，最终需转换为字符串形态传入。（东八区时间，单位为秒）',
	EndTimestamp int unsigned not null default 0 comment 'type为1时专用，表示结束时间，建议设置为截止日期的23:59:59过期。（东八区时间，单位为秒）',
	FixedTerm int not null default 0 comment 'type为2时专用，表示自领取后多少天内有效，领取后当天有效填写0。（单位为天）',
	FixedBeginTerm int not null default 0 comment 'type为2时专用，表示自领取后多少天开始生效。（单位为天）',
	
	UseCustomCode int not null default 0 comment '是否自定义Code码。1(true)或0(false)，默认为0。通常自有优惠码系统的开发者选择自定义Code码，在卡券投放时带入。',
	BindOpenId int not null default 0 comment '是否指定用户领取。1(true)或0(false)，默认为0。',
	ServicePhone varchar(24) default null comment '客服电话',
	LocationIdList text default null comment '门店位置ID。调用POI门店管理接口获取门店位置ID。例：1234，2312',
	Source varchar(36) default null comment '第三方来源名，例如同程旅游、大众点评。',
	CustomUrlName varchar(15) default null comment '自定义跳转外链的入口名字',
	CustomUrl varchar(128) default null comment '自定义跳转的URL',
	CustomUrlSubTitle varchar(18) default null comment '显示在入口右侧的提示语',
	PromotionUrlName varchar(15) default null comment '营销场景的自定义入口名称',
	PromotionUrl varchar(128) default null comment '入口跳转外链的地址链接',
	PromotionUrlSubTitle varchar(18) default null comment '显示在营销入口右侧的提示语。',
	GetLimit int not null default 1 comment '每人可领券的数量限制。不填默认与Quantity等值',
	CanShare int not null default 0 comment '卡券领取页面是否可分享。1(true)或0(false)，默认为0。',
	CanGiveFriend int not null default 0 comment '卡券是否可转赠。1(true)或0(false)，默认为0。',
	
	`State` int not null default 0 comment '卡券状态：0：审核中；1：通过；2：未通过；3：已删除',
	`DateTime` datetime not null comment '入库时间'
)comment '卡券';

drop table if exists groupon_card;
create table groupon_card
(
	CardGuid char(32) primary key not null comment '卡券Guid',
	DealDetail varchar(24) not null comment '团购券专用，团购详情'
)comment '团购券';

drop table if exists cash_card;
create table cash_card
(
	CardGuid char(32) primary key not null comment '卡券Guid',
	LeastCost int not null default 0 comment '代金券专用，表示起用金额。（单位为分）',
	ReduceCost int not null default 0 comment '代金券专用，表示减免金额。（单位为分）'
)comment '代金券';

drop table if exists discount_card;
create table discount_card
(
	CardGuid char(32) primary key not null comment '卡券Guid',
	Discount int not null default 0 comment '折扣券专用，表示打折额度（百分比）。填30就是七折'
)comment '折扣券';

drop table if exists gift_card;
create table gift_card
(
	CardGuid char(32) primary key not null comment '卡券Guid',
	Gift varchar(3072) not null comment '礼品券专用，填写礼品的名称'
)comment '礼品券';

drop table if exists generalcoupon_card;
create table generalcoupon_card
(
	CardGuid char(32) primary key not null comment '卡券Guid',
	DefaultDetail varchar(3072) not null comment '优惠券专用，填写优惠详情'
)comment '优惠券';

create table getcard
(
	Id int auto_increment primary key comment '主键',
	Guid char(32) not null comment 'Guid',
	CardId varchar(50) not null comment '领取的卡券Id',
	CardCode varchar(50) not null comment '领取的卡券Code',
	OpenId varchar(50) not null comment '领取人的微信OpenId',
	IsGiveByFriend int not null default 0 comment '是否为转赠：0：否；1：是；',
	FriendUserOpenId varchar(50) null comment '赠送方OpenID',
	OuterId int not null default 0 comment '领取场景值，0：无；1：扫描二维码领取；2：Js-Sdk添加领取',
	IsConsume int not null default 0 comment '是否核销该卡券，0：无；1：已核销',
	DateTime datetime not null comment '入库时间'
)comment '领取卡券';
