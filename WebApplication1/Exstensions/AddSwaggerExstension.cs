namespace Talabat.APIs.Exstensions
{
    public static class AddSwaggerExstension
    {
        public static WebApplication UseSwaggerMiddleWares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
