clc; clear all;

a1 = 0.2;
a2 = 0.25;

dh = [
    0   0   a1  0   0;
    0   0   a2  pi  0;
    0   0   0   0   1;
    0   0   0   0   0];

R = UnityLink(dh, 'name', 'ABB SCARA IRB');

R.qlim = [
    -deg2rad(140)       deg2rad(140);
    -deg2rad(150)       deg2rad(150);
    0                   0.18;
    -deg2rad(400)       deg2rad(400)];

dt = 0.01;
t1 = abs(R.qlim(1, 2) - R.qlim(1, 1));
t2 = abs(R.qlim(2, 2) - R.qlim(2, 1));
t3 = abs(R.qlim(3, 2) - R.qlim(3, 1));
t4 = abs(R.qlim(4, 2) - R.qlim(4, 1));

q1 = R.qlim(1, 1):0.01:R.qlim(1, 2);
q2 = R.qlim(2, 1):0.01:R.qlim(2, 2);
q3 = R.qlim(3, 1):0.01:R.qlim(3, 2);
q4 = R.qlim(4, 1):0.01:R.qlim(4, 2);

disp([length(q1) length(q2) length(q3) length(q4)])

pause();
for i=1:length(q1)
    %R.plot([q1(i) 0 0 0]);
    R.sendQ([q1(i) 0 0 0]);
end

for i=1:length(q2)
    %R.plot([q1(i) 0 0 0]);
    R.sendQ([0 q2(i) 0 0]);
end

R.plot([0 0 0 0]);
%R.teach;