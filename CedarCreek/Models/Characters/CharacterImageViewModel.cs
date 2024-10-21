namespace CedarCreek.Models.Characters
{
    public class CharacterImageViewModel
    {
        public Guid ImageID { get; set; }
        public String ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? CharacterID { get; set; }

    }
}
