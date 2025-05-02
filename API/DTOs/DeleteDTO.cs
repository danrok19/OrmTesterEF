namespace API.DTOs
{
    public class DeleteDTO
    {
        public List<long> OnlyEntity { get; set; } = new();
        public List<long> OneToOne { get; set; } = new();
        public List<long> OneToMany { get; set; } = new();
        public List<long> ManyToMany { get; set; } = new();

        public void AddOnlyEntity(long time)
        {
            OnlyEntity.Add(time);
        }

        public void AddOneToOne(long time)
        {
            OneToOne.Add(time);
        }

        public void AddOneToMany(long time)
        {
            OneToMany.Add(time);
        }

        public void AddManyToMany(long time)
        {
            ManyToMany.Add(time);
        }

        public override string ToString()
        {
            return $"DeleteDTO {{ OnlyEntity = [{string.Join(", ", OnlyEntity)}], " +
                   $"OneToOne = [{string.Join(", ", OneToOne)}], " +
                   $"OneToMany = [{string.Join(", ", OneToMany)}], " +
                   $"ManyToMany = [{string.Join(", ", ManyToMany)}] }}";
        }
    }
}
