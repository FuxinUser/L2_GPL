using System;


namespace DataMod.Common
{
    public class MMSMsgProResultModel
    {
        [Serializable]
        public class ProResult{
            public string No { get; set; }
            public string Result { get; set; }
            public string RejectCause { get; set; }

            public ProResult(string reqCoilNo, string proResult, string rejectCause = "")
            {
                this.No = reqCoilNo;
                this.Result = proResult;
                this.RejectCause = rejectCause;
            }

            public string DisplayResResult { get
                {
                    return Result.Equals("0") ? "成功" : "失敗";
                } }
        }
    }
}
