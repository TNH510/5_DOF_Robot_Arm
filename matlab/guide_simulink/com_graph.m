
s = serial('COM21'); 
set(s, 'BaudRate', 9600); 
fopen(s); 

figure;
plotData = []; 
while true
    if s.BytesAvailable > 0 
        data = fread(s, s.BytesAvailable); 
        plotData = [plotData; data'];
        plot(plotData);
        drawnow; 
    end
end

fclose(s);
delete(s);