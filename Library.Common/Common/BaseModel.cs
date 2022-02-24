using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Common
{
    public class BaseModel
    {
        public BaseModel():base()
        {
            CreateDate = DateTime.Now;
        }

        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
    }
}
