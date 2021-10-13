using System;
using System.Collections.Generic;

#nullable disable

namespace DemoSignalR.Models
{
    public partial class TinhThanh
    {
        public TinhThanh()
        {
            CaBenhs = new HashSet<CaBenh>();
        }

        public int MaTinh { get; set; }
        public string TenTinh { get; set; }

        public virtual ICollection<CaBenh> CaBenhs { get; set; }
    }
}
