namespace Sivenk;

public class Func
{
    /*public double FunRight(double x, double y, double gamma, double lambda) => FunU(x,y)*gamma;
    public double FunU(double x, double y) => x + y;
    public double Fun2kray(double x, double y) => 1;*/
    
    public double FunRight(double x, double y, double gamma, double lambda) => FunU(x,y)*gamma ;
    public double FunU(double x, double y) => x+y;
    public double Fun2kray(double x, double y) => 1;
}