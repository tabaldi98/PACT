namespace app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions
{
    public static class Guard
    {
        public static void ObjectNotFound<TObject>(TObject obj)
        {
            if (obj == null)
            {
                throw new ObjectNotFoundException<TObject>();
            }
        }

        public static void ObjectAlreadyExists<TObject>(bool assertion)
        {
            if (assertion)
            {
                throw new ObjectAlreadyExistsException<TObject>();
            }
        }
    }
}
