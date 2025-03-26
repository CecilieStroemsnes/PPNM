using System;
using System.Threading;
using System.Linq;

public class data { 
    public int a,b; 
    public double sum;
}

public class Program{
    public static void harm(object obj){
        var arg = (data)obj;
        arg.sum=0;
        for(int i=arg.a; i<arg.b; i++)arg.sum+=1.0/i;
        }

    public static void Main(string[] args){
        int nthreads = 1;
        int nterms = (int)1e8; /* default values */
        bool includeParallelForTests = false;
        
        foreach(var arg in args) {
            var words = arg.Split(':');
            if(words[0]=="-threads") 
                nthreads=int.Parse(words[1]);
            if(words[0]=="-terms"  ) 
                nterms  =(int)float.Parse(words[1]);
            if (words[0] == "-pitfalls" && words[1] == "true")
                includeParallelForTests = true;
        }

        data[] parameters = new data[nthreads];
        for(int i = 0; i < nthreads; i++) {
            parameters[i] = new data();
            parameters[i].a = 1 + nterms/nthreads*i;
            parameters[i].b = 1 + nterms/nthreads*(i+1);
        }
        parameters[parameters.Length-1].b=nterms+1; /* the enpoint might need adjustment */

        var threads = new System.Threading.Thread[nthreads];
        for(int i = 0; i < nthreads; i++) {
            threads[i] = new System.Threading.Thread(harm); /* create a thread */
            threads[i].Start(parameters[i]); /* run it with params[i] as argument to "harm" */
        }

        foreach(var thread in threads) 
            thread.Join();

        double total=0; 
        foreach(var p in parameters) 
            total+=p.sum;


        Console.WriteLine($"Harmonic sum 1 to {nterms} ≈ {total}");

        // ----------- Task 2 -----------

        if (includeParallelForTests) {
                Console.WriteLine();
                Console.WriteLine("Pitfalls in multiprocessing");
                Console.WriteLine("Parallel.For (wrong shared sum):");

                double wrongSum = 0;
                System.Threading.Tasks.Parallel.For(1, nterms + 1, i => {
                    double x = 1.0 / i;
                    Thread.SpinWait(50); // add tiny delay
                    wrongSum += x;
                }); // before: wrongSum += 1.0 / i
                Console.WriteLine($"[Parallel.For - wrong] Harmonic sum 1 to {nterms} ≈ {wrongSum}");

                var sum = new System.Threading.ThreadLocal<double>(() => 0, true);
                System.Threading.Tasks.Parallel.For(1, nterms + 1, i => sum.Value += 1.0 / i);
                double totalsum = sum.Values.Sum();
                Console.WriteLine($"[Parallel.For - right] Harmonic sum 1 to {nterms} ≈ {totalsum}");
            }


        // double wrongSum=0;
        // System.Threading.Tasks.Parallel.For( 1, nterms+1, (int i) => wrongSum+=1.0/i );   
        // Console.WriteLine($"[Parallel.For - wrong] Harmonic sum 1 to {nterms} ≈ {wrongSum}");
    
        // Parallel.For with ThreadLocal

        // var sum = new System.Threading.ThreadLocal<double>( ()=>0, trackAllValues:true);
        // System.Threading.Tasks.Parallel.For( 1, nterms+1, (int i)=>sum.Value+=1.0/i );
        // double totalsum=sum.Values.Sum();
        // Console.WriteLine($"[Parallel.For - right] Harmonic sum 1 to {nterms} ≈ {totalsum}");


    }

}

