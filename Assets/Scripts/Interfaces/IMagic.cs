namespace DefaultNamespace.Abstract_classes
{
    public interface IMagic
    {
        public float MaxMagic {get; set; }
        
        public float CurrentMagic { get; set; }
        
        public BasicMagicManager MagicManager { get; set; }
        
    }
}