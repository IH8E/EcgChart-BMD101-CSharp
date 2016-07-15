/*
 * Сделано в SharpDevelop.
 * Пользователь: IH8E
 * Дата: 15.07.2016
 * Время: 14:29
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using ZedGraph;
namespace EcgChart
{
	/// <summary>
	/// Class for drawing my chart with ZedGraph
	/// </summary>
	public class MyChart
	{
		private ZedGraphControl defaultGraph;
		private int pointCount;
		private string chartTitle = "ECG";
		
		public MyChart(ZedGraphControl g)
		{
			defaultGraph = g; 
			pointCount = 0; 
			initGraph();
		}
		private void initGraph(ZedGraphControl graph=null){
			graph=(graph!=null)?graph:defaultGraph;
			GraphPane pane = graph.GraphPane;
			pane.CurveList.Clear();
			pane.Title.Text = chartTitle;
			pane.YAxis.Title.IsVisible = false;
			pane.XAxis.Title.Text="Time";
//			pane.XAxis.Title.IsVisible = false;
//			pane.XAxis.Scale.Min = 0;
			LineItem myCurve = pane.AddCurve (chartTitle, null, Color.Blue, SymbolType.None);
			graph.IsEnableHZoom = true;
			graph.IsEnableVZoom = false;
			graph.IsShowHScrollBar = true;
			graph.IsAutoScrollRange = true;
			this.Update();
		}
		public void DrawGraph (List<double> val,string label,bool append)
		{
			label = (label!=null)?label:"ECG";
			Color color = Color.Blue;
			GraphPane pane = defaultGraph.GraphPane; 
			
			if(append==false) pane.CurveList.Clear (); 
			else color=Color.Maroon;
			pointCount = 0;
			PointPairList list = this.listToPointPairList(val); 
			LineItem myCurve = pane.AddCurve (label, list, color, SymbolType.None);
			this.Update();
		}
		private PointPairList listToPointPairList(List<double> val)
		{
			PointPairList result = new PointPairList ();
			int l=val.Count;
			for(int i=0; i<l;i++) {
				result.Add(i,val[i]);
			}
			return result;
		}
		private void Update() 
		{
			defaultGraph.AxisChange ();
			defaultGraph.Invalidate ();
		}
		public void AddPoint(double val) 
		{
			
		}
	}
}
