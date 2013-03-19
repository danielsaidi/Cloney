$(document).ready(function() {
  
	function setupExternalLinks() {
		$("a[rel='external']").attr("target", "_blank");
	}

	function updateGitHubInfo() {
		var api_base = "https://api.github.com/repos/danielsaidi/Cloney";
		var api_tags = api_base + "/tags?callback=?";

		updateVersionInfo("0.9.3.1");

		/*$.get(api_tags, function(data) {
			updateVersionInfo(data[0]["name"]);
		});*/
	}

	function updateVersionInfo(version) {
		$(".version-target").html(version);
	}

	setupExternalLinks();
	updateGitHubInfo();
});