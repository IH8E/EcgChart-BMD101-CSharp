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
		ZedGraphControl graph;
		GraphPane pane;
		const string chartTitle = "ECG";
		int pointCount;
		
		
		public MyChart(ZedGraphControl g)
		{
			graph = g; 
			pane = graph.GraphPane; 
			pointCount = 0; 
			initGraph();
		}
		void initGraph(){
			pane.CurveList.Clear();
			pane.Title.Text = chartTitle;
			pane.YAxis.Title.IsVisible = false;
			pane.XAxis.Title.Text="Time";
//			pane.XAxis.Title.IsVisible = false;
			pane.XAxis.Scale.Min = 0;
			graph.RestoreScale(pane); // restore to default all zooming, etc.
//			pane.XAxis.AxisGap
			LineItem myCurve = pane.AddCurve (chartTitle, null, Color.Blue, SymbolType.None);
			graph.IsEnableHZoom = true;
			graph.IsEnableVZoom = false;
			graph.IsShowHScrollBar = true;
			graph.IsAutoScrollRange = true;
			Update();
		}
		public void DrawGraph (List<double> val,string label,bool append)
		{
			label = label ?? "ECG";
			Color color = Color.Blue;
			
			if(!append) pane.CurveList.Clear (); 
			else color=Color.Maroon;
			pointCount = 0;
			PointPairList list = listToPointPairList(val); 
			LineItem myCurve = pane.AddCurve (label, list, color, SymbolType.None);
			graph.RestoreScale(pane);
			Update();
		}
		PointPairList listToPointPairList(List<double> val)
		{
			PointPairList result = new PointPairList ();
			int l=val.Count;
			for(int i=0; i<l;i++) {
				result.Add(i,val[i]);
			}
			return result;
		}
		void Update() 
		{
			graph.AxisChange ();
			graph.Invalidate ();
		}
		public void AddPoint(double val,int curveIndex=0) 
		{
			pane.CurveList[curveIndex].AddPoint(pointCount++,val);
			Update();
		}
	}
}
