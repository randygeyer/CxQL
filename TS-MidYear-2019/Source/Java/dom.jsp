<html>
<head><title>First JSP</title></head>
<body>
	<input type="hidden" name="BankingCenterId" value="${param['strBankingCenterId']}" /> <!-- FN Reflected XSS -->
	<input type="hidden" name="offerCode" value="${param.offerCode}"/>  <!-- TP Reflected XSS -->
</body>
</html>