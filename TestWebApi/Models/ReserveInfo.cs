using System;
using System.Collections.Generic;

namespace TestWebApi.Models
{
    public partial class ReserveInfo
    {
        /// <summary>
        /// 識別號碼
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 流水號
        /// </summary>
        public int SID { get; set; }
        /// <summary>
        /// 預約號碼
        /// </summary>
        public string ReserveID { get; set; }
        /// <summary>
        /// 預約人員姓名
        /// </summary>
        public string ReserveUserName { get; set; }
        /// <summary>
        /// 預約人員電話
        /// </summary>
        public string ReserveUserPhone { get; set; }
        /// <summary>
        /// 預約人數
        /// </summary>
        public short NumberOfPeople { get; set; }
        /// <summary>
        /// 預約日期
        /// </summary>
        public DateTime ReserveDate { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
