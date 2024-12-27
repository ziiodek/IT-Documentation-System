using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ITDocumentation
{
    public class SYM : Author
    {
        public int ID { set; get; }
        public string? LPAR { set; get; }
        public string? SymNo { set; get; }
        public string? DiskQuantity { set; get; }
        public string? SizeMB { set; get; }
        public string? VersionSP { set; get; }
        public string? SystemDate { set; get; }

        public string? SYMCreationDate { set; get;}

        public string? Status { set; get; }

        public string? Owner { set; get; }

        public string? PointContact { set; get; }

        public string? SymFunctionality { set; get; }
        public string? DevicesConfigured { set; get; }

        public string? CurrentTesting { set; get; }
    
        public string? ActiveCWRTested { set; get; }

        public string? PhysicalLocation { set; get; }
        public string? Notes { set; get; }
        

       
    }
}
