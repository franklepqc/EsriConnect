using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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
            .AddTransient<IConstructeurUrl, ConstructeurUrl>()
            .AddTransient<IParametresRequete, ParametresRequete>()
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
            var actuel = client.Obtenir<DistrictEsri>(URLBASE);

            // Assert.
            Assert.AreEqual(attendu, actuel.Count());
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client")]
        public void ObtenirUnSeulDistrictSansConversion()
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
            var actuel = client.Obtenir<DistrictEsri>(URLBASE, parametres).FirstOrDefault();

            // Assert.
            Assert.IsNotNull(actuel);
            Assert.AreEqual(noSecteurAttendu, actuel.LECODEXVILLID);
            Assert.AreEqual(nomSecteurAttendu, actuel.LENOM);
            Assert.AreEqual(abbreviationAttendu, actuel.LEABREV);
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
            var actuel = client.Obtenir<DistrictEsri>(URLBASE, parametres).FirstOrDefault();

            // Assert.
            Assert.IsNotNull(actuel);
            Assert.AreEqual(noSecteurAttendu, actuel.LECODEXVILLID);
            Assert.AreEqual(nomSecteurAttendu, actuel.LENOM);
            Assert.AreEqual(abbreviationAttendu, actuel.LEABREV);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client")]
        public void ObtenirUnSeulDistrictAvecId()
        {
            // Variables de travail.
            var client = _serviceCollection.GetService<IClient>();
            var parametres = _serviceCollection.GetService<IParametresRequete>();
            parametres.Where = @"LECODEXVILLID = 25";
            parametres.ChampsSorties = new List<string>() { "LECODEXVILLID" };

            // Attendu.
            var noSecteurAttendu = 25;

            // Actuel.
            var actuel = client.Obtenir<DistrictEsriId>(URLBASE, parametres).FirstOrDefault();

            // Assert.
            Assert.IsNotNull(actuel);
            Assert.AreEqual(noSecteurAttendu, actuel.LECODEXVILLID);
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
            var actuel = client.Obtenir<DistrictEsri>(URLBASE, parametres).FirstOrDefault();

            // Assert.
            Assert.IsNotNull(actuel);
            Assert.AreEqual(noSecteurAttendu, actuel.LECODEXVILLID);
            Assert.AreEqual(nomSecteurAttendu, actuel.LENOM);
            Assert.AreEqual(abbreviationAttendu, actuel.LEABREV);
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
            var actuel = client.Obtenir<DistrictEsri>(URLBASE, parametres).FirstOrDefault();

            // Assert.
            Assert.IsNotNull(actuel);
            Assert.AreEqual(noSecteurAttendu, actuel.LECODEXVILLID);
            Assert.AreEqual(nomSecteurAttendu, actuel.LENOM);
            Assert.AreEqual(abbreviationAttendu, actuel.LEABREV);
        }

        /// <summary>
        /// Classe pour le district (retour d'ESRI - id seul).
        /// </summary>
        public class DistrictEsriId
        {
            public byte LECODEXVILLID { get; set; }
        }

        /// <summary>
        /// Classe pour le district (retour d'ESRI).
        /// </summary>
        public class DistrictEsri : DistrictEsriId
        {
            public string LENOM { get; set; }
            public string LEABREV { get; set; }
        }
    }
}
