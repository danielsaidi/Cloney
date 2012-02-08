$(document).ready(function() {
  $("a[rel='external']").attr("target", "_blank");
  
  var repo = new gh.repo("danielsaidi", "Cloney");
  var tags = repo.getLatestRelease(function(result){ $("#version").html(result); });
});