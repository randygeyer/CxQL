DECLARE
	v_query VARCHAR2(200);
	v_output VARCHAR2(200);
	v_x VARCHAR2(100); 
BEGIN
	select x INTO v_x from table1;
	if DBMS_ASSERT.TABLE_NAME(v_x) = v_x then
		v_query := 'update table2 t2 set t2.x = ' + v_x;
		EXECUTE IMMEDIATE v_query INTO v_output;
	end if;
END;