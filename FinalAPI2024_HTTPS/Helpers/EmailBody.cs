using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalAPI2024_HTTPS.Helpers
{
    public static class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return $@"<html>
                <head></head>
<body style="" margin:0;padding:0;font-family:Arial, Helvetica, sans-serif;"">
<div style="" height: auto;backgorund:linear-gradient(to top, #c9c9ff 50%, #6e6ef6 90%) no-repeat;width:400px;padding:30px"">
<div>
<div>
<h1 >Cambia tu contraseña</h1>
<hr>
<p>Porfavor da clic en el botón de abajo para establecer una nueva contraseña</p>
<a href=""https://serviruta.eu/#/reset?email={email}&code={emailToken}"" target=""_blank"" style=""background:#0d6efd;padding:10px;border:none;
color:white;border-radius:4px;display:block;margin:0 auto;width:50%;text-align:center;text-decoration:none"">Restablecer contraseña</a><br>

<p>Saludos Cordiales,<br><br>
ServirUTA
</p>
</div>
</div>
</div>
</body>
</html>
";

        }
    }
}
