using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using ToDo.Business.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI.TagHelpers
{
    [HtmlTargetElement("getirGorevAppUserId")]
    public class GorevAppUserIdTagHelper : TagHelper
    {
        private readonly IGorevService _gorevService;

        public GorevAppUserIdTagHelper(IGorevService gorevService)
        {
            _gorevService = gorevService;
        }

        public int AppUserId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<Gorev> gorevler = _gorevService.GetirAppUserId(AppUserId);
            int tamamlananGorevSayisi = gorevler.Where(x => x.Durum).Count();
            int ustundeCalistigiGorevSayisi = gorevler.Where(x => !x.Durum).Count();

            string htmlString =
                $"<div>Tamamlanan Görev Sayısı : <span class='badge bg-success'>{tamamlananGorevSayisi}</span></div>" +
                $"<div>Üstünde Çalıştığı Görev Sayısı : <span class='badge bg-danger'>{ustundeCalistigiGorevSayisi}</span></div>";

            output.Content.SetHtmlContent(htmlString);
        }
    }
}
