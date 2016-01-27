using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Wechat.WebUI
{
    // 摘要:
    //      微信支付接口
    //  
    public static class Pay
    {
        // 摘要: 
        //     生成订单号。
        //
        // 参数: 
        //   mchID:
        //     微信商户平台商户号。
        //
        // 返回结果: 订单贸易号（string）
        //
        public static string GetOutTradeNo(string mchID)
        {
            return string.Format("{0}{1}{2}", mchID, DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(999));
        }

        // 摘要: 
        //     生成签名。
        //
        // 参数: 
        //   orderParams:
        //     发送或者接收到的数据集合M按照ASCII码字典序集合。
        //
        //   mchAPISecret:
        //     微信商户平台商户支付密钥
        //     mchAPISecret设置路径：微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置。
        //
        // 返回结果: 签名（string）
        //
        public static string GetSign(SortedDictionary<string, object> orderParams, string mchAPISecret)
        {
            //1.将字典序集合内非空参数值的参数，使用URL键值对的格式（即key1=value1&key2=value2…）拼接成字符串stringA
            string stringA = Common.SortedDictionaryToUrl(orderParams);

            //2.在stringA最后拼接上key得到stringSignTemp字符串
            string stringSignTemp = string.Format("{0}&key={1}", stringA, mchAPISecret);//加入密钥

            //3.对stringSignTemp进行MD5运算
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(stringSignTemp)); // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择

            //4.通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            var sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                sb.Append(s[i].ToString("x2"));// 将得到的字符串使用十六进制类型格式。//在此处可以使用X，x,X2,x2 区别是：X：转换后是16进制大写；x：转换后是16进制小写；2：每次都是两位数比如（0x0A如果没有2,就只会输出0xA）
            }

            //将加密后得到的所有字符转换为大写
            return sb.ToString().ToUpper();
        }

        // 摘要: 
        //     统一下单。
        //
        // 注释：
        //      传输方式	为保证交易安全性，采用HTTPS传输
        //      提交方式	采用POST方法提交
        //      数据格式	提交和返回数据都为XML格式，根节点名为xml
        //      字符编码	统一采用UTF-8字符编码
        //      签名算法	MD5，后续会兼容SHA1、SHA256、HMAC等。
        //      签名要求	请求和接收数据均需要校验签名，详细方法请参考安全规范-签名算法
        //      证书要求	调用申请退款、撤销订单接口需要商户证书
        //      判断逻辑	先判断协议字段返回，再判断业务返回，最后判断交易状态
        //
        // 参数: 
        //   orderParams:
        //     发送或者接收到的数据集合M按照ASCII码字典序集合。
        //
        // 返回结果: xml转换成的SortedDictionary
        //     <xml>
        //          <return_code><![CDATA[SUCCESS]]></return_code>
        //          <return_msg><![CDATA[OK]]></return_msg>
        //          <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
        //          <mch_id><![CDATA[10000100]]></mch_id>
        //          <nonce_str><![CDATA[IITRi8Iabbblz1Jc]]></nonce_str>
        //          <sign><![CDATA[7921E432F65EB8ED0CE9755F0E86D72F]]></sign>
        //          <result_code><![CDATA[SUCCESS]]></result_code>
        //          <prepay_id><![CDATA[wx201411101639507cbf6ffd8b0779950874]]></prepay_id>
        //          <trade_type><![CDATA[JSAPI]]></trade_type>
        //      </xml>
        //
        public static SortedDictionary<string, object> UnifiedOrder(SortedDictionary<string, object> orderParams)
        {
            var resultXml = WebHttpClient.Post("https://api.mch.weixin.qq.com/pay/unifiedorder", Common.SortedDictionaryToXml(orderParams));

            return Common.XmlToSortedDictionary(resultXml);
        }

        // 摘要: 
        //     查询订单。
        // 
        // 注释：
        //      传输方式	为保证交易安全性，采用HTTPS传输
        //      提交方式	采用POST方法提交
        //      数据格式	提交和返回数据都为XML格式，根节点名为xml
        //      字符编码	统一采用UTF-8字符编码
        //      签名算法	MD5，后续会兼容SHA1、SHA256、HMAC等。
        //      签名要求	请求和接收数据均需要校验签名，详细方法请参考安全规范-签名算法
        //      证书要求	调用申请退款、撤销订单接口需要商户证书
        //      判断逻辑	先判断协议字段返回，再判断业务返回，最后判断交易状态
        //
        // 参数: 
        //   orderParams:
        //     发送或者接收到的数据集合M按照ASCII码字典序集合。
        //
        // 返回结果: xml转换成的SortedDictionary
        //     <xml>
        //          <return_code><![CDATA[SUCCESS]]></return_code>
        //          <return_msg><![CDATA[OK]]></return_msg>
        //          <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
        //          <mch_id><![CDATA[10000100]]></mch_id>
        //          <device_info><![CDATA[1000]]></device_info>
        //          <nonce_str><![CDATA[TN55wO9Pba5yENl8]]></nonce_str>
        //          <sign><![CDATA[BDF0099C15FF7BC6B1585FBB110AB635]]></sign>
        //          <result_code><![CDATA[SUCCESS]]></result_code>
        //          <openid><![CDATA[oUpF8uN95-Ptaags6E_roPHg7AG0]]></openid>
        //          <is_subscribe><![CDATA[Y]]></is_subscribe>
        //          <trade_type><![CDATA[MICROPAY]]></trade_type>
        //          <bank_type><![CDATA[CCB_DEBIT]]></bank_type>
        //          <total_fee>1</total_fee>
        //          <fee_type><![CDATA[CNY]]></fee_type>
        //          <transaction_id><![CDATA[1008450740201411110005820873]]></transaction_id>
        //          <out_trade_no><![CDATA[1415757673]]></out_trade_no>
        //          <attach><![CDATA[订单额外描述]]></attach>
        //          <time_end><![CDATA[20141111170043]]></time_end>
        //          <trade_state><![CDATA[SUCCESS]]></trade_state>
        //     </xml>
        //
        public static SortedDictionary<string, object> QueryOrder(SortedDictionary<string, object> orderParams)
        {
            var resultXml = WebHttpClient.Post("https://api.mch.weixin.qq.com/pay/orderquery", Common.SortedDictionaryToXml(orderParams));

            return Common.XmlToSortedDictionary(resultXml);
        }

        // 摘要: 
        //     关闭订单。
        //
        // 注释：
        //      商户订单支付失败需要生成新单号重新发起支付，要对原订单号调用关单，避免重复支付；系统下单后，用户支付超时，系统退出不再受理，避免用户继续，请调用关单接口。
        // 
        // 注释：
        //      传输方式	为保证交易安全性，采用HTTPS传输
        //      提交方式	采用POST方法提交
        //      数据格式	提交和返回数据都为XML格式，根节点名为xml
        //      字符编码	统一采用UTF-8字符编码
        //      签名算法	MD5，后续会兼容SHA1、SHA256、HMAC等。
        //      签名要求	请求和接收数据均需要校验签名，详细方法请参考安全规范-签名算法
        //      证书要求	调用申请退款、撤销订单接口需要商户证书
        //      判断逻辑	先判断协议字段返回，再判断业务返回，最后判断交易状态
        //
        // 注意：
        //      订单生成后不能马上调用关单接口，最短调用时间间隔为5分钟。
        //
        // 参数: 
        //   orderParams:
        //     发送或者接收到的数据集合M按照ASCII码字典序集合。
        //
        // 返回结果: xml转换成的SortedDictionary
        //      <xml>
        //          <return_code><![CDATA[SUCCESS]]></return_code>
        //          <return_msg><![CDATA[OK]]></return_msg>
        //          <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
        //          <mch_id><![CDATA[10000100]]></mch_id>
        //          <nonce_str><![CDATA[BFK89FC6rxKCOjLX]]></nonce_str>
        //          <sign><![CDATA[72B321D92A7BFA0B2509F3D13C7B1631]]></sign>
        //          <result_code><![CDATA[SUCCESS]]></result_code>
        //      </xml>
        //
        public static SortedDictionary<string, object> CloseOrder(SortedDictionary<string, object> orderParams)
        {
            var resultXml = WebHttpClient.Post("https://api.mch.weixin.qq.com/pay/closeorder", Common.SortedDictionaryToXml(orderParams));

            return Common.XmlToSortedDictionary(resultXml);
        }

        // 摘要: 
        //     申请退款。
        // 
        // 注释：
        //      传输方式	为保证交易安全性，采用HTTPS传输
        //      提交方式	采用POST方法提交
        //      数据格式	提交和返回数据都为XML格式，根节点名为xml
        //      字符编码	统一采用UTF-8字符编码
        //      签名算法	MD5，后续会兼容SHA1、SHA256、HMAC等。
        //      签名要求	请求和接收数据均需要校验签名，详细方法请参考安全规范-签名算法
        //      证书要求	调用申请退款、撤销订单接口需要商户证书
        //      判断逻辑	先判断协议字段返回，再判断业务返回，最后判断交易状态
        //
        // 注意：
        //      交易时间超过一年的订单无法提交退款。
        //      微信支付退款支持单笔交易分多次退款，多次退款需要提交原支付订单的商户订单号和设置不同的退款单号。一笔退款失败后重新提交，要采用原来的退款单号。总退款金额不能超过用户实际支付金额
        //
        // 参数: 
        //   orderParams:
        //     发送或者接收到的数据集合M按照ASCII码字典序集合。
        //
        // 返回结果: xml转换成的SortedDictionary
        //      <xml>
        //          <return_code><![CDATA[SUCCESS]]></return_code>
        //          <return_msg><![CDATA[OK]]></return_msg>
        //          <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
        //          <mch_id><![CDATA[10000100]]></mch_id>
        //          <nonce_str><![CDATA[NfsMFbUFpdbEhPXP]]></nonce_str>
        //          <sign><![CDATA[B7274EB9F8925EB93100DD2085FA56C0]]></sign>
        //          <result_code><![CDATA[SUCCESS]]></result_code>
        //          <transaction_id><![CDATA[1008450740201411110005820873]]></transaction_id>
        //          <out_trade_no><![CDATA[1415757673]]></out_trade_no>
        //          <out_refund_no><![CDATA[1415701182]]></out_refund_no>
        //          <refund_id><![CDATA[2008450740201411110000174436]]></refund_id>
        //          <refund_channel><![CDATA[]]></refund_channel>
        //          <refund_fee>1</refund_fee>
        //          <coupon_refund_fee>0</coupon_refund_fee>
        //      </xml>
        //
        public static SortedDictionary<string, object> RefundOrder(SortedDictionary<string, object> orderParams)
        {
            var resultXml = WebHttpClient.Post("https://api.mch.weixin.qq.com/secapi/pay/refund", Common.SortedDictionaryToXml(orderParams));

            return Common.XmlToSortedDictionary(resultXml);
        }

        // 摘要: 
        //     查询退款单。
        // 
        // 注释：
        //      传输方式	为保证交易安全性，采用HTTPS传输
        //      提交方式	采用POST方法提交
        //      数据格式	提交和返回数据都为XML格式，根节点名为xml
        //      字符编码	统一采用UTF-8字符编码
        //      签名算法	MD5，后续会兼容SHA1、SHA256、HMAC等。
        //      签名要求	请求和接收数据均需要校验签名，详细方法请参考安全规范-签名算法
        //      证书要求	调用申请退款、撤销订单接口需要商户证书
        //      判断逻辑	先判断协议字段返回，再判断业务返回，最后判断交易状态
        //
        // 注意：
        //      提交退款申请后，通过调用该接口查询退款状态。退款有一定延时，用零钱支付的退款20分钟内到账，银行卡支付的退款3个工作日后重新查询退款状态。
        //
        // 参数: 
        //   orderParams:
        //     发送或者接收到的数据集合M按照ASCII码字典序集合。
        //
        // 返回结果: xml转换成的SortedDictionary
        //      <xml>
        //          <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
        //          <mch_id><![CDATA[10000100]]></mch_id>
        //          <nonce_str><![CDATA[TeqClE3i0mvn3DrK]]></nonce_str>
        //          <out_refund_no_0><![CDATA[1415701182]]></out_refund_no_0>
        //          <out_trade_no><![CDATA[1415757673]]></out_trade_no>
        //          <refund_count>1</refund_count>
        //          <refund_fee_0>1</refund_fee_0>
        //          <refund_id_0><![CDATA[2008450740201411110000174436]]></refund_id_0>
        //          <refund_status_0><![CDATA[PROCESSING]]></refund_status_0>
        //          <result_code><![CDATA[SUCCESS]]></result_code>
        //          <return_code><![CDATA[SUCCESS]]></return_code>
        //          <return_msg><![CDATA[OK]]></return_msg>
        //          <sign><![CDATA[1F2841558E233C33ABA71A961D27561C]]></sign>
        //          <transaction_id><![CDATA[1008450740201411110005820873]]></transaction_id>
        //      </xml>
        //
        public static SortedDictionary<string, object> QueryRefundOrder(SortedDictionary<string, object> orderParams)
        {
            var resultXml = WebHttpClient.Post("https://api.mch.weixin.qq.com/pay/refundquery", Common.SortedDictionaryToXml(orderParams));

            return Common.XmlToSortedDictionary(resultXml);
        }

        // 摘要: 
        //     下载对账单。
        // 
        // 注释：
        //      传输方式	为保证交易安全性，采用HTTPS传输
        //      提交方式	采用POST方法提交
        //      数据格式	提交和返回数据都为XML格式，根节点名为xml
        //      字符编码	统一采用UTF-8字符编码
        //      签名算法	MD5，后续会兼容SHA1、SHA256、HMAC等。
        //      签名要求	请求和接收数据均需要校验签名，详细方法请参考安全规范-签名算法
        //      证书要求	调用申请退款、撤销订单接口需要商户证书
        //      判断逻辑	先判断协议字段返回，再判断业务返回，最后判断交易状态
        //
        // 注意：
        //      1、微信侧未成功下单的交易不会出现在对账单中。支付成功后撤销的交易会出现在对账单中，跟原支付单订单号一致，bill_type为REVOKED。
        //      2、微信在次日9点启动生成前一天的对账单，建议商户10点后再获取。
        //      3、对账单中涉及金额的字段单位为“元”。
        //
        // 参数: 
        //   orderParams:
        //     发送或者接收到的数据集合M按照ASCII码字典序集合。
        //
        // 返回结果: 
        //      成功时：
        //          数据以文本表格的方式返回，第一行为表头，后面各行为对应的字段内容，字段内容跟查询订单或退款结果一致，具体字段说明可查阅相应接口。
        //      失败时：
        //          <xml>
        //              <return_code><![CDATA[FAIL]]></return_code>
        //              <return_msg><![CDATA[签名失败]]></return_msg>
        //          </xml>
        //
        public static SortedDictionary<string, object> DownloadBill(SortedDictionary<string, object> orderParams)
        {
            var resultXml = WebHttpClient.Post("https://api.mch.weixin.qq.com/pay/downloadbill", Common.SortedDictionaryToXml(orderParams));

            if (resultXml.Substring(0, 5) == "<xml>") //若接口调用失败会返回xml格式的结果
                return Common.XmlToSortedDictionary(resultXml);
            else  //接口调用成功则返回非xml格式的数据
            {
                var result = new SortedDictionary<string, object>();
                result.Add("result", resultXml);
                return result;
            }
        }

        // 摘要: 
        //     测速上报。
        // 
        // 注释：
        //      传输方式	为保证交易安全性，采用HTTPS传输
        //      提交方式	采用POST方法提交
        //      数据格式	提交和返回数据都为XML格式，根节点名为xml
        //      字符编码	统一采用UTF-8字符编码
        //      签名算法	MD5，后续会兼容SHA1、SHA256、HMAC等。
        //      签名要求	请求和接收数据均需要校验签名，详细方法请参考安全规范-签名算法
        //      证书要求	调用申请退款、撤销订单接口需要商户证书
        //      判断逻辑	先判断协议字段返回，再判断业务返回，最后判断交易状态
        //
        // 注意：
        //      商户在调用微信支付提供的相关接口时，会得到微信支付返回的相关信息以及获得整个接口的响应时间。为提高整体的服务水平，协助商户一起提高服务质量，微信支付提供了相关接口调用耗时和返回信息的主动上报接口，微信支付可以根据商户侧上报的数据进一步优化网络部署，完善服务监控，和商户更好的协作为用户提供更好的业务体验。
        //
        // 参数: 
        //   orderParams:
        //     发送或者接收到的数据集合M按照ASCII码字典序集合。
        //
        // 返回结果: 
        //      <xml>
        //          <return_code><![CDATA[SUCCESS/FAIL]]></return_code>
        //          <return_msg><![CDATA[返回信息，如非空，为错误原因（签名失败/参数格式校验错误）]]></return_msg>
        //      </xml>
        //
        public static SortedDictionary<string, object> Report(SortedDictionary<string, object> orderParams)
        {
            var resultXml = WebHttpClient.Post("https://api.mch.weixin.qq.com/payitil/report", Common.SortedDictionaryToXml(orderParams));

            return Common.XmlToSortedDictionary(resultXml);
        }

        // 摘要: 
        //     接收从微信支付后台发送过来的数据并验证签名。
        //
        // 注意：
        //      操作后，微信支付平台会把相关支付结果发送给商户，商户需要接收处理，并返回应答。
        //
        // 返回结果: 微信支付后台返回的数据
        //
        public static SortedDictionary<string, object> GetNotifyData()
        {
            //接收从微信后台POST过来的数据
            var inputStream = HttpContext.Current.Request.InputStream;

            var data = new StringBuilder();

            try
            {
                int count = 0;

                byte[] buffer = new byte[1024];

                while ((count = inputStream.Read(buffer, 0, 1024)) > 0)
                {
                    data.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
            }
            catch (Exception ex)
            {
                //通知管理员，支付接口问题
                return ErrorInfo(ex.Message);
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Flush();
                    inputStream.Close();
                    inputStream.Dispose();
                }
            }

            return Common.XmlToSortedDictionary(data.ToString());
        }

        // 摘要: 
        //     支付异常消息。
        //
        // 参数: 
        //   error:
        //     错误信息。
        //
        // 返回结果: SortedDictionary<string, object>
        //
        public static SortedDictionary<string, object> ErrorInfo(string error)
        {
            var resultPrams = new SortedDictionary<string, object>();

            resultPrams.Add("return_code", "FAIL");
            resultPrams.Add("return_msg", error);

            return resultPrams;
        }

        // 摘要: 
        //     支付结果通用通知。
        //
        // 返回结果: 
        //      <xml>
        //          <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
        //          <attach><![CDATA[支付测试]]></attach>
        //          <bank_type><![CDATA[CFT]]></bank_type>
        //          <fee_type><![CDATA[CNY]]></fee_type>
        //          <is_subscribe><![CDATA[Y]]></is_subscribe>
        //          <mch_id><![CDATA[10000100]]></mch_id>
        //          <nonce_str><![CDATA[5d2b6c2a8db53831f7eda20af46e531c]]></nonce_str>
        //          <openid><![CDATA[oUpF8uMEb4qRXf22hE3X68TekukE]]></openid>
        //          <out_trade_no><![CDATA[1409811653]]></out_trade_no>
        //          <result_code><![CDATA[SUCCESS]]></result_code>
        //          <return_code><![CDATA[SUCCESS]]></return_code>
        //          <sign><![CDATA[B552ED6B279343CB493C5DD0D78AB241]]></sign>
        //          <sub_mch_id><![CDATA[10000100]]></sub_mch_id>
        //          <time_end><![CDATA[20140903131540]]></time_end>
        //          <total_fee>1</total_fee>
        //          <trade_type><![CDATA[JSAPI]]></trade_type>
        //          <transaction_id><![CDATA[1004400740201409030005092168]]></transaction_id>
        //     </xml>
        //
        public static SortedDictionary<string, object> PayResultNotify()
        {
            var result = GetNotifyData();

            if (result["return_code"].ToString() == "SUCCESS")
            {
                if (!string.IsNullOrEmpty(result["transaction_id"].ToString()))
                    return ErrorInfo("支付结果中微信订单号不存在");

                string transaction_id = result["transaction_id"].ToString();

                //查询订单，判断订单真实性
                var orderPrams = new SortedDictionary<string, object>();
                orderPrams.Add("appid", Model.ApiModel.AppID);
                orderPrams.Add("mch_id",Model.ApiModel.MchID);
                orderPrams.Add("transaction_id", transaction_id);
                orderPrams.Add("nonce_str", Common.GetNonceStr());
                orderPrams.Add("sign", Pay.GetSign(orderPrams, Model.ApiModel.MchAPISecret));
                var queryResult = QueryOrder(orderPrams);
                if (queryResult["return_code"].ToString() != "SUCCESS" || queryResult["result_code"].ToString() != "SUCCESS")
                    return ErrorInfo("订单查询失败"); //若订单查询失败，则立即返回结果给微信支付后台
            }

            return result;
        }

        // 摘要: 
        //      生成预支付URL-扫码支付模式一。
        //
        // 注意：
        //      需要设置支付回调URL。进入公众平台-->微信支付-->开发配置-->扫码支付-->修改。URL需要实现的功能：接收用户扫码后微信支付系统回调的productid和openid
        //
        // 步骤：
        //      1、商户后台系统根据微信支付规则链接生成二维码，链接中带固定参数productid（可定义为产品标识或订单号）。
        //      2、用户扫码后，微信支付系统将productid和用户唯一标识(openid)回调商户后台系统(需要设置支付回调URL)，商户后台系统根据productid生成支付交易，最后微信支付系统发起用户支付流程。
        // 
        // 参数: 
        //   orderParams:
        //     发送或者接收到的数据集合M按照ASCII码字典序集合。
        //
        //   orderParams需要包含的字段：
        //      公众账号ID	appid	String(32)	是	wx8888888888888888	微信分配的公众账号ID
        //      商户号	mch_id	String(32)	是	1900000109	微信支付分配的商户号
        //      时间戳	time_stamp	String(10)	是	1414488825	系统当前时间，定义规则详见时间戳
        //      随机字符串	nonce_str	String(32)	是	5K8264ILTKCH16CQ2502SI8ZNMTM67VS	随机字符串，不长于32位。推荐随机数生成算法
        //      商品ID	product_id	String(32)	是	88888	商户定义的商品id 或者订单号
        //      签名	sign	String(32)	是	C380BEC2BFD727A4B6845133519F3AD6	签名，详见签名生成算法
        //
        // 返回结果: 支付URl（string）
        //
        public static string GetPayUrlForNativeOne(SortedDictionary<string, object> orderParams)
        {
            return "weixin://wxpay/bizpayurl?" + Common.SortedDictionaryToUrl(orderParams, true);
        }

        // 摘要: 
        //      生成直接支付URL-扫码支付模式二。
        // 
        // 注意：
        //      不需要设置支付回调URL
        //
        // 步骤：
        //      1、商户后台系统调用微信支付【统一下单API】生成预付交易，将接口返回的链接生成二维码。
        //      2、用户扫码后输入密码完成支付交易。注意：该模式的预付单有效期为2小时，过期后无法支付。
        // 
        // 参数: 
        //   orderParams:
        //     发送或者接收到的数据集合M按照ASCII码字典序集合。
        //
        // 返回结果: 支付URl（string）
        //
        public static string GetPayUrlForNativeTwo(SortedDictionary<string, object> orderParams)
        {
            var result = UnifiedOrder(orderParams);//调用统一下单

            return result["code_url"].ToString();//获得统一下单接口返回的二维码链接
        }
    }
}
