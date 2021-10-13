using System;
using System.Collections.Generic;

#nullable disable

namespace DemoSignalR.Models
{
    public partial class CaBenh
    {
        public int MaTinh { get; set; }
        public int Id { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public int? GioCapNhat { get; set; }
        public int? SoCaNhiemBenh { get; set; }
        public int? SoCaHoiPhuc { get; set; }
        public int? SoCaTuVong { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual TinhThanh MaTinhNavigation { get; set; }
    }
}
