using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace OrderService.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public int? DriverId { get; set; }
        [Required]
        public Point StartDest { get; set; }
        [Required]
        public Point EndDest { get; set; }
        [Required]
        public double Fee { get; set; }
        [Required]
        public status Status { get; set; }
    }

    public enum status
    {
        waiting,
        accepted,
        finished,
    }
}