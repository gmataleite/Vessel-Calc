using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VesselCalc.Domain.Entities;

namespace VesselCalc.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedMaterialsAsync(ApplicationDbContext context, string csvFilePath)
        {
            if (await context.Materials.AnyAsync())
                return;

            var lines = await File.ReadAllLinesAsync(csvFilePath);
            var materialsToInsert = new List<Material>();
            
            // Ignora o cabeçalho (linha 0)
            for (int i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(';');
                if (columns.Length < 13) continue;

                // CultureInfo.InvariantCulture garante que pontos sejam lidos corretamente
                var material = new Material(
                    specification: columns[0],
                    grade: columns[1],
                    productForm: columns[2],
                    minTensile: decimal.Parse(columns[3], CultureInfo.InvariantCulture),
                    minYield: decimal.Parse(columns[4], CultureInfo.InvariantCulture)
                );

                // Temperaturas mapeadas nas colunas (T100 está na coluna 5, T200 na 6, etc.)
                decimal[] temperatures = { 100, 200, 300, 400, 500, 600, 700, 800 };
                
                for (int t = 0; t < temperatures.Length; t++)
                {
                    int columnIndex = t + 5;
                    // Só adiciona se houver valor válido no CSV para aquela temperatura
                    if (!string.IsNullOrWhiteSpace(columns[columnIndex]))
                    {
                        var stress = decimal.Parse(columns[columnIndex], CultureInfo.InvariantCulture);
                        material.AddAllowableStress(temperatures[t], stress);
                    }
                }

                materialsToInsert.Add(material);
            }

            await context.Materials.AddRangeAsync(materialsToInsert);
            await context.SaveChangesAsync();
        }
    }
}