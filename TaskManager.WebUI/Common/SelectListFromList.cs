using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Application.Common.Models;

namespace TaskManager.WebUI.Common
{
    public static class SelectListFromList
    {
        public static SelectList ToSelectList<T>(this IList<T> list)
        {
            return new SelectList(list, nameof(FrameDto.Value), nameof(FrameDto.Name));
        }

        public static SelectList ToSelectList<T>(this IList<T> list, object selectedValue)
        {
            return new SelectList(list, nameof(FrameDto.Value), nameof(FrameDto.Name), selectedValue);
        }
    }
}
