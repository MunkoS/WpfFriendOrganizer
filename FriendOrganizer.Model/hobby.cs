

using System.ComponentModel.DataAnnotations;


namespace FriendOrganizer.Model
{
    public class Hobby
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? TypeHobbyId { get; set; }

        public TypeHobby TypeHobby { get; set; }
    }
}
