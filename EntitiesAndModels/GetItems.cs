using System.Collections.Generic;

namespace EntitiesAndModels
{
    public class GetItems
    {
        public int ID_Main { get; set; }
        public string No_KK { get; set; }
        public string Tanggal_Terbit { get; set; }
        public string Nama_KK { get; set; }
        public string Alamat { get; set; }
        public List<GetItemsDetail> Details { get; set; }
    }
}
