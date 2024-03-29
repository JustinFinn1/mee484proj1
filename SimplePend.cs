//============================================================================
// SimplePend.cs Defines a class for simulating a simple pendulum 
//============================================================================
using System;

namespace Sim
{
	public class SimplePend
	{
		private double len = 1.1; // pendulum length (m)
		private double g = 9.81;	// gravitation field strength (m/s^2)
		int n = 2; 					// number of states
		private double[] x; 		// array of states 
		private double[] f; 		// right side of equation evaluated

		//--------------------------------------------------------------------
		// constructor
		//--------------------------------------------------------------------
		public SimplePend()
		{
			x = new double[n];
			f = new double [n];

			x[0] = 1.0;
			x[1] = 0.0;
		}
		
		//--------------------------------------------------------------------
		// step: perform one integration step via Euler's Method
		//		 Soon, it will implement RK4
		//--------------------------------------------------------------------
		public void step(double dt)
		{
			rhsFunc(x,f);

			for(int i=0;i<n;++i)
			{
				x[i] = x[i] + f[i] * dt;
			}

		}

		//--------------------------------------------------------------------
		// rhsFunc: function to calculate rhs of pendulum ODEs
		//--------------------------------------------------------------------
		public void rhsFunc(double[] st, double[] ff)
		{
			ff[0] = st[1];
			ff[1] = -g/len * Math.Sin(st[0]);
		}

		//--------------------------------------------------------------------
		// rk4 method
		//--------------------------------------------------------------------
		public void rk4(double dt)
		{
			double [] xi = new double[n];
			double [,] k = new double[4,2] {{0.0, 0.0}, {0.0, 0.0}, {0.0, 0.0}, {0.0, 0.0}};

			int i;

			//k1
			rhsFunc(x, f);
			for(i = 0; i < n; ++i)
			{
				k[0, i] = f[i];
				xi[i] = x[i] + k[0, i] * 0.5 * dt;
			}

			//k2
			rhsFunc(xi, f);
			for(i = 0; i < n; ++i)
			{
				k[1, i] = f[i];
				xi[i] = x[i] + k[1, i] * 0.5 * dt;
			}

			//k3
			rhsFunc(xi, f);
			for(i = 0; i < n; ++i)
			{
				k[2, i] = f[i];
				xi[i] = x[i] + k[2, i] * 0.5 * dt;
			}

			//k4
			rhsFunc(xi, f);
			for(i = 0; i < n; ++i)
			{
				k[3, i] = f[i];
				xi[i] = x[i] + k[3, i] * 0.5 * dt;
			}

			//new value calculation
			for(i = 0; i < n; ++i)
			{
				x[i] = x[i] + (k[0, i] + 2 * k[1, i] + 2 * k[2, i] + k[3, i] )* dt/6;
			}
			
		}

		//--------------------------------------------------------------------
		// Getters and setters
		//--------------------------------------------------------------------
		public double L
		{
			get {return(len);}

			set
			{
				if(value > 0.0)
					len = value;
			}
		}

		public double G
		{
			get {return(g);}

			set
			{
				if(value > 0.0)
					g = value;
			}
		}

		public double theta
		{
			get {return x[0];}

			set {x[0] = value;}
		}

		public double thetaDot
		{
			get {return x[1];}

			set {x[1] = value;}
		}

	}

}