﻿using LE.UserService.Infrastructure.Infrastructure;
using LE.UserService.Infrastructure.Infrastructure.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LE.UserService.Services.Implements
{
    public class LangService : ILangService
    {
        private LanggeneralDbContext _context;
        public LangService(LanggeneralDbContext context)
        {
            _context = context;
        }
        public async Task InitLanguage()
        {
            if (_context.Languages.FirstOrDefault() != null)
                return;
            var filename = "Jsonfiles/language.json";
            var text = File.ReadAllText(filename);
            var langdictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
            var languages = langdictionary.Select(x => new Language { Langid = System.Guid.NewGuid(), LocaleCode = x.Key, Name = x.Value });
            await _context.AddRangeAsync(languages);
            await _context.SaveChangesAsync();
        }
    }
}