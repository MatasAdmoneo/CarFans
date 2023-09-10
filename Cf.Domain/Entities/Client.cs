namespace Cf.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }

        public Client(string name)
        {
            Name = name;
        }
    }
}
