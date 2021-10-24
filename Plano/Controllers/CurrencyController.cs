using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        /// <summary>
        /// 够根据输入货币的货币代码和值 [Amount] 和目标货币 [TargetCode]代码得到目标货币的值
        /// </summary>
        /// <param name="CurrCode">输入货币</param>
        /// <param name="Amount">输入值</param>
        /// <param name="TargetCode">目标货币</param>
        /// <returns>处理结果 Status--处理状态 Message--说明 Result--目标货币的值</returns>
        [HttpGet("CurrencyConvert")]
        public Response Convert(string CurrCode,string Amount,string TargetCode)
        {
            Response rsp = new Response();
            decimal amount = 0m;
            if (string.IsNullOrEmpty(CurrCode) || string.IsNullOrEmpty(TargetCode) || !Decimal.TryParse(Amount, out amount))
            {
                rsp.Message = "参数不全或参数格式错误！";
                return rsp;
            }
            if (amount <= 0)
            {
                rsp.Message = "Amount需大于0！";
                return rsp;
            }
            if (CurrCode.Equals(TargetCode))
            {
                rsp.Message = "无需转换";
                return rsp;
            }
            DataContext db = new DataContext();
            try
            {
                //转换的结果
                string result = "";

                CurrencyProps targetp = db.CurrencyProps.Where(m => m.CurrencyCode.Equals(TargetCode)).FirstOrDefault();
                if (targetp == null || targetp.CPID <= 0)
                {
                    throw new Exception("TargetCode的值不存在！");
                }
                result = targetp.Symbol.Trim().ToUpper();

                ExchangeRates baseRates = db.ExchangeRates.Where(m => m.IsDefault).FirstOrDefault();
                if (baseRates == null)
                {
                    throw new Exception("无默认汇率！");
                }

                ExchangeRates currRates = db.ExchangeRates.Where(m => m.CurrencyCode.Equals(CurrCode)).FirstOrDefault();
                if (currRates == null)
                {
                    throw new Exception("CurrCode无对应汇率！");
                }
                ExchangeRates targetRates = db.ExchangeRates.Where(m => m.CurrencyCode.Equals(TargetCode)).FirstOrDefault();
                if (targetRates == null)
                {
                    throw new Exception("TargetCode无对应汇率！");
                }
                if (targetRates.ExchangeRate <=0 || currRates.ExchangeRate <= 0)
                {
                    throw new Exception("汇率错误！");
                }

                //默认货币金额
                decimal baseAmount = (amount * baseRates.ExchangeRate) / currRates.ExchangeRate;

                //目标货币金额
                decimal targetAmount = baseAmount * targetRates.ExchangeRate;

                //格式化返回值
                targetAmount = Math.Round(targetAmount, targetp.DecimalPlace);
                result = result + targetAmount.ToString();

                //返回对象赋值
                rsp.Result = result;
                rsp.Status = true;
            }
            catch(Exception ex)
            {
                rsp.Message = ex.Message;
            }
            return rsp;
        }
    }
}
