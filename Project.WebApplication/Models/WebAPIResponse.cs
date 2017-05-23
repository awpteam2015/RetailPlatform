using System;
using NHibernate.Mapping.ByCode.Impl.CustomizersImpl;

namespace Project.WebApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    [Serializable]
    public class WebAPIResponse<TResult>
    {


        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }


        /// <summary>
        /// 返回结果
        /// </summary>
        public TResult Result { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public ErrorInfo Error { get; set; }


        public WebAPIResponse()
        {

        }

        public WebAPIResponse(Tuple<bool, TResult> returnResult)
        {
            if (returnResult.Item1)
            {
                Success = true;
                Result = returnResult.Item2;
            }
            else
            {
                Success = false;
                Error = new ErrorInfo() { Message = returnResult.Item2 == null ? "" : returnResult.Item2.ToString() };
            }
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="result"></param>
        public WebAPIResponse(TResult result)
        {
            Result = result;
            Success = true;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="error"></param>
        public WebAPIResponse(ErrorInfo error)
        {
            Error = error;
            Success = false;
        }

    }
}