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
        private readonly string URLBASE = @"https://sdgis.ville.gatineau.qc.ca/agsweb/rest/services/General/DistrictCanu/MapServer/3";

        /// <summary>
        /// Injection de d�pendances.
        /// </summary>
        private IServiceProvider _serviceCollection = new ServiceCollection()
            .AddTransient<IConstructeurUrl, ConstructeurUrl>()
            .AddTransient<IParametresRequete, ParametresRequete>()
            .AddScoped<IRepoFeatureClass, RepoFeatureClass>()
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
            Assert.AreEqual(noSecteurAttendu, actuel.attributes.LECODEXVILLID);
            Assert.AreEqual(nomSecteurAttendu, actuel.attributes.LENOM);
            Assert.AreEqual(abbreviationAttendu, actuel.attributes.LEABREV);
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
            Assert.AreEqual(noSecteurAttendu, actuel.attributes.LECODEXVILLID);
            Assert.AreEqual(nomSecteurAttendu, actuel.attributes.LENOM);
            Assert.AreEqual(abbreviationAttendu, actuel.attributes.LEABREV);
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
            Assert.AreEqual(noSecteurAttendu, actuel.attributes.LECODEXVILLID);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client")]
        public void ObtenirUnSeulDistrictViaLatLong()
        {
            // Variables de travail.
            var client = _serviceCollection.GetService<IClient>();
            var parametres = _serviceCollection.GetService<IParametresRequete>();
            parametres.Point = new PointLatLong(45.39518215539051d, -75.84492306335648d);
            parametres.AfficherTousLesChamps = true;

            // Attendu.
            var noSecteurAttendu = 25;
            var nomSecteurAttendu = "Aylmer";
            var abbreviationAttendu = "A";

            // Actuel.
            var actuel = client.Obtenir<DistrictEsri>(URLBASE, parametres).FirstOrDefault();

            // Assert.
            Assert.IsNotNull(actuel);
            Assert.AreEqual(noSecteurAttendu, actuel.attributes.LECODEXVILLID);
            Assert.AreEqual(nomSecteurAttendu, actuel.attributes.LENOM);
            Assert.AreEqual(abbreviationAttendu, actuel.attributes.LEABREV);
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
            parametres.Point = new Point
            {
                X = 356089.315529d,
                Y = 5028569.06737d,
                SRID = 32189
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
            Assert.AreEqual(noSecteurAttendu, actuel.attributes.LECODEXVILLID);
            Assert.AreEqual(nomSecteurAttendu, actuel.attributes.LENOM);
            Assert.AreEqual(abbreviationAttendu, actuel.attributes.LEABREV);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client")]
        public void ObtenirUnSeulDistrictViaXYEtDeuxChamps()
        {
            // Variables de travail.
            var client = _serviceCollection.GetService<IClient>();
            var parametres = _serviceCollection.GetService<IParametresRequete>();
            parametres.Point = new Point
            {
                X = 356089.315529d,
                Y = 5028569.06737d,
                SRID = 32189
            };
            parametres.AfficherTousLesChamps = false;
            parametres.ChampsSorties = new[] { nameof(DistrictEsri.LECODEXVILLID), nameof(DistrictEsri.LENOM) };

            // Attendu.
            var noSecteurAttendu = 25;
            var nomSecteurAttendu = "Aylmer";
            var abbreviationAttendu = null as string;

            // Actuel.
            var actuel = client.Obtenir<DistrictEsri>(URLBASE, parametres).FirstOrDefault();

            // Assert.
            Assert.IsNotNull(actuel);
            Assert.AreEqual(noSecteurAttendu, actuel.attributes.LECODEXVILLID);
            Assert.AreEqual(nomSecteurAttendu, actuel.attributes.LENOM);
            Assert.AreEqual(abbreviationAttendu, actuel.attributes.LEABREV);
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
