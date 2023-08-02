namespace RecMe.Models
{
    public class ThingHasTag
    {
        public int Id { get; set; }
        public int ThingId { get; set; }
        public int TagId { get; set; }

        public ThingHasTag() { }

        public ThingHasTag(int thing, int tag)
        {
            ThingId = thing;
            TagId = tag;
        }
    }
}
