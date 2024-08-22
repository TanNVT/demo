using System;

namespace demoWebApp.Entities.IDateTracking
{

    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public interface IDateTracking
    {
        public DateTime? CreatedDate { get; set; }


        public DateTime? ModifiedDate { get; set; }
    }
}