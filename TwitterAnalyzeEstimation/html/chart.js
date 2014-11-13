google.load("visualization", "1", {packages:["corechart"]});
google.setOnLoadCallback( 
    function () { 
        var data = google.visualization.arrayToDataTable(dataArr);
        var options = {
            title: titleTxt,
			hAxis: {title: '極性',
				    viewWindow: {min:-100, max:100},
					gridlines:{color:'transparent'}}, 
			vAxis: {title: 'ツイート件数'},
			legend: 'none',
			lineWidth: 1,
			pointSize: 0
		};

		var chart = new google.visualization.ScatterChart(document.getElementById('chart'));
		chart.draw(data, options);

		// 分析結果の出力
		document.getElementById('average').innerHTML = String(average);
		document.getElementById('variance').innerHTML = String(variance) + '<br/>(' + String(standardDeviation) + ')';
		var clusterData = [[0, cluster[0]["average"]], [1, cluster[1]["average"]], [2, cluster[2]["average"]]];
		for(var i = 0; i < 2; i++) {
			for(var j = 0; j < 2; j++) {
				if(clusterData[j][1] < clusterData[j + 1][1]) {
					var x = clusterData[j];
					clusterData[j] = clusterData[j + 1];
					clusterData[j + 1] = x;
				}
			}
		}
		var index = clusterData[0][0];
		document.getElementById('cluster_positive_count').innerHTML = String(cluster[index]["count"]);
		document.getElementById('cluster_positive_average').innerHTML = String(cluster[index]["average"]);
		document.getElementById('cluster_positive_variance').innerHTML = String(cluster[index]["variance"]) + '<br/>(' + String(cluster[index]["standardDeviation"]) + ')';
		document.getElementById('cluster_positive_word').innerHTML = "";
		for(var i = 0; i < cluster[index]["wordRanking"].length && i < 20; i++) {
		    document.getElementById('cluster_positive_word').innerHTML += "<p>" + String(i + 1) + ": " + cluster[index]["wordRanking"][i]["word"] + "(" + cluster[index]["wordRanking"][i]["value"] + ")</p>";
		}
		index = clusterData[1][0];
		document.getElementById('cluster_normal_count').innerHTML = String(cluster[index]["count"]);
		document.getElementById('cluster_normal_average').innerHTML = String(cluster[index]["average"]);
		document.getElementById('cluster_normal_variance').innerHTML = String(cluster[index]["variance"]) + '<br/>(' + String(cluster[index]["standardDeviation"]) + ')';
		document.getElementById('cluster_normal_word').innerHTML = "";
		for(var i = 0; i < cluster[index]["wordRanking"].length && i < 20; i++) {
		    document.getElementById('cluster_normal_word').innerHTML += "<p>" + String(i + 1) + ": " + cluster[index]["wordRanking"][i]["word"] + "(" + cluster[index]["wordRanking"][i]["value"] + ")</p>";
		}
		index = clusterData[2][0];
		document.getElementById('cluster_negative_count').innerHTML = String(cluster[index]["count"]);
		document.getElementById('cluster_negative_average').innerHTML = String(cluster[index]["average"]);
		document.getElementById('cluster_negative_variance').innerHTML = String(cluster[index]["variance"]) + '<br/>(' + String(cluster[index]["standardDeviation"]) + ')';
		document.getElementById('cluster_negative_word').innerHTML = "";
		for(var i = 0; i < cluster[index]["wordRanking"].length && i < 20; i++) {
		    document.getElementById('cluster_negative_word').innerHTML += "<p>" + String(i + 1) + ": " + cluster[index]["wordRanking"][i]["word"] + "(" + cluster[index]["wordRanking"][i]["value"] + ")</p>";
		}

	}
);