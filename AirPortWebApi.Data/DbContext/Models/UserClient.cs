using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AirPortWebApi.Data.DbContext.Models
{
    public class UserClient
    {
        public string Id { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
        public string RefreshTokenId { get; set; }
        public virtual RefreshToken RefreshToken { get; set; }
    }
}
