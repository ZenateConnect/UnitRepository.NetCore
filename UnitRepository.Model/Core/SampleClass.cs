using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnitRepository.Model.Core
{
    [Table("SampleClass")]
    public class SampleClass : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(Constant.ConstantEntity.LengthFirstName)]
        public string FirstName { get; set; }

        [StringLength(Constant.ConstantEntity.LengthLastName)]
        public string LastName { get; set; }

        [NotMapped]
        [StringLength(Constant.ConstantEntity.LengthFullName)]
        public string FullName
        {
            get
            {
                return string.Concat(FirstName, " ", LastName);
            }
        }


    }
}
