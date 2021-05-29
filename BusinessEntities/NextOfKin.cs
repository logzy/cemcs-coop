namespace COOP.Banking.BusinessEntities
{
    public class NextOfKin
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Category { get; set; }
        public int ParentPersonId { get; set; }

        public Person Person { get; set; }
    }
}
