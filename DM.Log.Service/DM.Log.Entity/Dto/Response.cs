namespace DM.Log.Entity
{
    public class Response
    {
        public long Code { get; set; }

        public string Desc { get; set; } = "";
    }


    public class Response<T>: Response
    {
        public T Data { get; set; }

        public Response<T> SetSuccess(T data)
        {
            this.Code = 200;
            this.Desc = "Success.";

            this.Data = data;

            return this;
        }
    }
}

