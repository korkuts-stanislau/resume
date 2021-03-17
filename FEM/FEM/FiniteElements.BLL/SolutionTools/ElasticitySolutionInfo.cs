namespace FiniteElements.BLL.SolutionTools
{
    public class ElasticitySolutionInfo
    {
        public double Modulus { get; set; }
        public double Coefficient { get; set; }
        public override string ToString()
        {
            return $"Модуль Юнга {Modulus}, Коэффициент Пуассона {Coefficient}";
        }
    }
}
