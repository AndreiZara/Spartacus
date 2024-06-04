using System.ComponentModel.DataAnnotations;

namespace Spartacus.Domain.Enums
{
    public enum MsDuration : int
    {
        [Display(Name = "One month")]
        OneMonth = 1,
        [Display(Name = "Three months")]
        ThreeMonths = 3,
        [Display(Name = "Six months")]
        SixMonths = 6,
        [Display(Name = "One year")]
        OneYear = 12
    }
}
