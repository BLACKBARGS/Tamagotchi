namespace TamagotchiPropriedades
{
    public class PropriedadesTamagotchi
    {
        public string Nome { get; private set; }
        public int Fome { get; private set; }
        public int Felicidade { get; private set; }
        public int Alegria { get; private set; }
        public int Idade { get; private set; }
        public int Energia { get; private set; }
        public int Saude { get; private set; }
        public int Inteligencia { get; private set; }

        public PropriedadesTamagotchi(string nome)
        {
            Nome = nome;
            Fome = 100;
            Felicidade = 100;
            Alegria = 100;
            Idade = 0;
            Energia = 100;
            Saude = 100;
            Inteligencia = 50;
        }
    }
}