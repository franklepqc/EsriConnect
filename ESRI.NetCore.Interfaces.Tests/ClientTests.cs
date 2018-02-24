using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Net.Http;

namespace ESRI.NetCore.Interfaces.Tests
{
    /// <summary>
    /// Tests pour la construction des tests.
    /// </summary>
    [TestClass]
    public class ClientTests
    {
        /// <summary>
        /// Url de base pour l'interrogation.
        /// </summary>
        private readonly string URLBASE = @"https://sdgis.ville.gatineau.qc.ca/agsweb/rest/services/General/DistrictCanu/MapServer/2";

        /// <summary>
        /// Injection de dépendances.
        /// </summary>
        private IServiceProvider _serviceCollection = new ServiceCollection()
            .AddSingleton<IMapper>((srv) =>
            {
                var config = new MapperConfiguration((cfg) =>
                {
                    cfg.CreateMap<DistrictT, DistrictU>()
                        .ForMember(k => k.Id, (srcMapping) => srcMapping.MapFrom(q => q.LECODEXVILLID))
                        .ForMember(k => k.Nom, (srcMapping) => srcMapping.MapFrom(q => q.LENOM))
                        .ForMember(k => k.Abbreviation, (srcMapping) => srcMapping.MapFrom(q => q.LEABREV));
                });

                return new Mapper(config);
            })
            .AddScoped<IParametresRequete, ParametresRequete>()
            .AddScoped<IClient, Client>()
            .BuildServiceProvider();

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client")]
        public void Obtenir()
        {
            // Variables de travail.
            var client = _serviceCollection.GetService<IClient>();

            // Attendu.
            var attendu = 5;

            // Actuel.
            var actuel = client.Obtenir<DistrictT, DistrictU>(URLBASE);

            // Assert.
            Assert.AreEqual(attendu, actuel.Count());
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client")]
        public void ObtenirUnSeulDistrict()
        {
            // Variables de travail.
            var client = _serviceCollection.GetService<IClient>();
            var parametres = _serviceCollection.GetService<IParametresRequete>();
            parametres.Where = @"LECODEXVILLID = 25";
            parametres.AfficherTousLesChamps = true;

            // Attendu.
            var noSecteurAttendu = 25;
            var nomSecteurAttendu = "Aylmer";
            var abbreviationAttendu = "A";

            // Actuel.
            var actuel = client.Obtenir<DistrictT, DistrictU>(URLBASE, parametres).FirstOrDefault();

            // Assert.
            Assert.IsNotNull(actuel);
            Assert.AreEqual(noSecteurAttendu, actuel.Id);
            Assert.AreEqual(nomSecteurAttendu, actuel.Nom);
            Assert.AreEqual(abbreviationAttendu, actuel.Abbreviation);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client")]
        public void ObtenirUnSeulDistrictViaLongLat()
        {
            // Variables de travail.
            var client = _serviceCollection.GetService<IClient>();
            var parametres = _serviceCollection.GetService<IParametresRequete>();
            parametres.Point = new PointLongLat
            {
                Longitude = -75.84492306335648d,
                Latitude = 45.39518215539051d
            };
            parametres.AfficherTousLesChamps = true;

            // Attendu.
            var noSecteurAttendu = 25;
            var nomSecteurAttendu = "Aylmer";
            var abbreviationAttendu = "A";

            // Actuel.
            var actuel = client.Obtenir<DistrictT, DistrictU>(URLBASE, parametres).FirstOrDefault();

            // Assert.
            Assert.IsNotNull(actuel);
            Assert.AreEqual(noSecteurAttendu, actuel.Id);
            Assert.AreEqual(nomSecteurAttendu, actuel.Nom);
            Assert.AreEqual(abbreviationAttendu, actuel.Abbreviation);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client")]
        public void ObtenirUnSeulDistrictViaXY()
        {
            // Variables de travail.
            var client = _serviceCollection.GetService<IClient>();
            var parametres = _serviceCollection.GetService<IParametresRequete>();
            parametres.Point = new PointXY
            {
                X = 356089.315529d,
                Y = 5028569.06737d
            };
            parametres.AfficherTousLesChamps = true;

            // Attendu.
            var noSecteurAttendu = 25;
            var nomSecteurAttendu = "Aylmer";
            var abbreviationAttendu = "A";

            // Actuel.
            var actuel = client.Obtenir<DistrictT, DistrictU>(URLBASE, parametres).FirstOrDefault();

            // Assert.
            Assert.IsNotNull(actuel);
            Assert.AreEqual(noSecteurAttendu, actuel.Id);
            Assert.AreEqual(nomSecteurAttendu, actuel.Nom);
            Assert.AreEqual(abbreviationAttendu, actuel.Abbreviation);
        }

        /// <summary>
        /// Classe pour le district (retour d'ESRI).
        /// </summary>
        public class DistrictT
        {
            public byte LECODEXVILLID { get; set; }
            public string LENOM { get; set; }
            public string LEABREV { get; set; }
        }

        /// <summary>
        /// Classe pour le district (retour).
        /// </summary>
        public class DistrictU
        {
            public byte Id { get; set; }
            public string Nom { get; set; }
            public string Abbreviation { get; set; }
        }

    }
}
