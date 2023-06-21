using System;

/**
 * Author:ICSC SPYUA
 * Data:2019/12/28
 * Desc:基底Model介面
 */
namespace DBService.Base
{
    public interface IRepositoryModel
    {
        /// <summary>
        /// 流水序號ID，最小值為1
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 此筆記錄最後變更時間
        /// </summary>
        DateTime UpdateTime { get; set; }

        /// <summary>
        /// 此筆記錄新增時間
        /// </summary>
        DateTime CreateTime { get; set; }
    }

}
