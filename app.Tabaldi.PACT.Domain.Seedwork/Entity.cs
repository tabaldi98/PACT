namespace app.Tabaldi.PACT.Domain.Seedwork
{
    public interface IEntity
    {
        int ID { get; }
    }

    public abstract class Entity : IEntity
    {
        private int _id;

        public virtual int ID
        {
            get => _id;
            set => _id = value;
        }

        #region Overrides Methods
        public override bool Equals(object obj)
        {
            return obj != null && obj is Entity && this == (Entity)obj;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(Entity entity1, Entity entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            if (entity1.ID.ToString().Equals(entity2.ID.ToString()))
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Entity entity1, Entity entity2)
        {
            return (!(entity1 == entity2));
        }

        #endregion Overrides Methods
    }
}
