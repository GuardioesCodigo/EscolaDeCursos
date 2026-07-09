using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EscolaDeCursos.WebApp.Compartilhado.Extensions;

public static class ModelStateExtensions
{
    public static void AddModelError(this ModelStateDictionary modelState, ResultBase result)
    {
        foreach (IError erro in result.Errors)
        {
            string campo =
                erro.Metadata["Campo"] is string ? erro.Metadata["Campo"].ToString()! : string.Empty;

            modelState.AddModelError(campo, erro.Message);
        }
    }
}
