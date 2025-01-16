namespace CedarCreek.Models.Realms
{
    public class RealmDeleteViewModel
    {
        public Guid ID { get; set; }
        public string RealmName { get; set; }
        public RealmEffect RealmEffect { get; set; }
        public int CharacterLevelRequirement { get; set; }
        public List<RealmImageViewModel> Image { get; set; } = new List<RealmImageViewModel>();

    }
}

