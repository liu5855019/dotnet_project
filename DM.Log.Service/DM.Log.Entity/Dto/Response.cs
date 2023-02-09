namespace DM.Log.Entity
{
    public class Response
    {
        public long Code { get; set; }

        public string Message { get; set; } = "";
    }


    public class Response<T>: Response
    {
        public T Data { get; set; }

        public Response<T> SetSuccess(T data)
        {
            this.Code = 200;
            this.Message = "Success.";
            this.Data = data;

            return this;
        }

        public Response<T> SetFailed(string message, long code = 500)
        {
            this.Code = code;
            this.Message = message;

            return this;
        }
    }
}

