using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TESTBNI.Models
{
    [Table("tb_m_barang")]
    public class Barang
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Quantity { get; set; }

        public string Deskripsi { get; set; }

        public string Harga { get; set; }
    }
}
