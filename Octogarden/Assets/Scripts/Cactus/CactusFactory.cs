using System;

public static class CactusFactory
{
    private static WeightedRandom<CactusClass> _cactusClassRandom;
    private static WeightedRandom<CactusFlowerColor> _cactusFlowerColorRandom;

    private static string[] possibleNames = new string[] { "Cecilia", "Oriol", "Columba", "Otilia", "Salvio", "Reinaldo", "Salomón", "Ceferino", "Fausto", "Colombo", "Leonardo", "Balduino", "Úrsula", "Guadalupe", "Vidal", "Alba", "Lázaro", "Ángel", "Claudio", "Pascual", "Probo", "Valentín", "Teodora", "Fulgencio", "Verónica", "Olga", "Óscar", "Josep", "Baltasar", "Encarnación", "Alejandra", "Lucía", "Eduardo", "Nicanor", "Gregorio", "Bernarda", "Homero", "Javier", "Germán", "Áurea", "Ramón", "Laureano", "Adalberto", "Sansón", "Julio", "Moisés", "Eugenia", "Renato", "Priscila", "Begoña", "Facundo", "Albert", "Constantino", "Carolina", "Míriam", "Amadeo", "Salomé", "Cristóbal", "Acacio", "Alejandro", "Fabián", "Elena", "Marina", "Bárbara", "Arcadio", "Gloria", "Tadeo", "Onésimo", "Venancio", "Sergio", "Bruno", "Alfonso", "Calixto", "Daciano", "Clotilde", "Erico", "Nuria", "Gustavo", "Adelaida", "Ascensión", "Roque", "Montserrat", "Nicomedes", "Asunción", "Jerónimo", "Fortunato", "Ubaldo", "Godofredo", "Lorenzo", "Aristides", "Inocencio", "Abrahán", "Gerardo", "Juan", "Liduvina", "Benedicto", "Ignacio", "Toribio", "Isidro", "Ildefonso", "Borja", "Tito", "Margarita", "Edgar", "Lucas", "Fernando", "Benito", "Cándida", "Remedios", "Clara", "Enrique", "Siro", "Evaristo", "Virgilio", "Isabel", "Octavio", "Celia", "Amparo", "Adón", "Raimundo", "Artemio", "Cayo", "Timoteo", "Casio", "Matías", "Rigoberto", "Oseas", "Ambrosio", "Martín", "Álvaro", "Rufo", "Casiano", "Ladislao", "Segismundo", "Jacob", "Bartolomé", "Norberto", "Manuel", "Rogelio", "Natividad", "Lorena", "Daniel", "Narciso", "Dacio", "Macario", "Cleofás", "Xavier", "Ifigenia", "Anatolio", "Jordi", "Salvador", "Jaume", "Irene", "Raquel", "Aurora", "Mateo", "Anselmo", "Jacobo", "José", "Soledad", "Silvia", "Almudena", "Carmelo", "Magdalena", "Joaquín", "Pío", "Bernabé", "Aitor", "Sixto", "Julián", "Ireneo", "Lidia", "Damián", "Epifanía", "Catalina", "Anastasia", "Tobías", "Alfredo", "Cesáreo", "Cayetano", "Lucrecia", "Nicolás", "Rebeca", "Rosa", "Celina", "Ángela", "Juana", "Ramiro", "Cristian", "Sebastián", "Marcos", "Jacinto", "Francisca", "Romualdo", "Rita", "Leoncio", "Francesc", "Constancio", "Santiago", "Camilo" };

    static CactusFactory()
    {
        _cactusClassRandom = new WeightedRandom<CactusClass>();
        _cactusClassRandom.Add(CactusClass.Melee, 1f);
        _cactusClassRandom.Add(CactusClass.Ranged, 1f);
        _cactusClassRandom.Add(CactusClass.Tank, 1f);

        _cactusFlowerColorRandom = new WeightedRandom<CactusFlowerColor>();
        _cactusFlowerColorRandom.Add(CactusFlowerColor.Red, 1f);
        _cactusFlowerColorRandom.Add(CactusFlowerColor.Yellow, 1f);
        _cactusFlowerColorRandom.Add(CactusFlowerColor.Orange, 1f);
        _cactusFlowerColorRandom.Add(CactusFlowerColor.White, 0.25f);
        _cactusFlowerColorRandom.Add(CactusFlowerColor.Magenta, 0.25f);
        _cactusFlowerColorRandom.Add(CactusFlowerColor.Blue, 0.25f);
        _cactusFlowerColorRandom.Add(CactusFlowerColor.Black, 0.1f);
        _cactusFlowerColorRandom.Add(CactusFlowerColor.Rainbow, 0.05f);
    }

    public static CactusData CreateCactus(CactusClass? cactusClass = null)
    {
        cactusClass = cactusClass ?? GetRandomCactusClass();
        CactusData data = new CactusData(
            GetRandomName(),
            cactusClass.Value,
            GetRandomMaxHealthForClass(cactusClass.Value),
            GetRandomAttackIntervalForClass(cactusClass.Value),
            GetRandomAttackDamageForClass(cactusClass.Value)
        );

        data.FlowerColor = GetRandomFlowerColor();

        return data;
    }

    private static CactusClass GetRandomCactusClass()
    {
        return _cactusClassRandom.Draw();
    }

    private static string GetRandomName()
    {
        int index = UnityEngine.Random.Range(0, possibleNames.Length);
        return possibleNames[index];
    }

    private static uint GetRandomMaxHealthForClass(CactusClass cactusClass)
    {
        switch (cactusClass)
        {
            case CactusClass.Melee:
                return (uint)UnityEngine.Random.Range(90, 110);
            case CactusClass.Ranged:
                return (uint)UnityEngine.Random.Range(60, 80);
            case CactusClass.Tank:
                return (uint)UnityEngine.Random.Range(140, 165);
            default:
                throw new ArgumentException("Invalid CactusClass");
        }
    }

    private static float GetRandomAttackIntervalForClass(CactusClass cactusClass)
    {
        switch (cactusClass)
        {
            case CactusClass.Melee:
                return UnityEngine.Random.Range(0.75f, 0.85f);
            case CactusClass.Ranged:
                return UnityEngine.Random.Range(1.5f, 1.7f);
            case CactusClass.Tank:
                return UnityEngine.Random.Range(1.0f, 1.65f);
            default:
                throw new ArgumentException("Invalid CactusClass");
        }
    }

    private static uint GetRandomAttackDamageForClass(CactusClass cactusClass)
    {
        switch (cactusClass)
        {
            case CactusClass.Melee:
                return (uint)UnityEngine.Random.Range(30, 40);
            case CactusClass.Ranged:
                return (uint)UnityEngine.Random.Range(18, 22);
            case CactusClass.Tank:
                return (uint)UnityEngine.Random.Range(25, 35);
            default:
                throw new ArgumentException("Invalid CactusClass");
        }
    }

    private static CactusFlowerColor GetRandomFlowerColor()
    {
        return _cactusFlowerColorRandom.Draw();
    }
}