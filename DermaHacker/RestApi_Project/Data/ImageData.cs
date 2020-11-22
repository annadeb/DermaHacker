using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Dicom.Data
{
    public class ImageData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public int[] CoordinateXY { get; set; }


        public FromMatlab Matlab { get; set; }

        [Required]
        public string Base64 { get; set; }

        public bool Status { get; set; }

        public Guid guid = Guid.NewGuid();

        public void IsFinish()
        {
            Status = true;
        }
    }

    public class FromMatlab
    {

        public string Width { get; set; }

        public string Height { get; set; }

        public string Arena { get; set; }
        public string MatrixC1 { get; set; }
        public string MatrixC2 { get; set; }
        public string MatrixC3 { get; set; }
    }
}
