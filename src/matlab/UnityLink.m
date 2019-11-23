classdef UnityLink < SerialLink
    properties
        ip
        port
        timeout
    end
    
    properties (Access = private)
        tcpipClient
    end
    
    methods
        function r = UnityLink(varargin)
        %SerialLink Creat a UnityLink robot object
            
            % default properties
            default.ip = '192.168.0.10';
            default.port = 8888;
            default.timeout = 30;
            
            % process the rest of the arguments in key, value pairs
            opt.ip = [];
            opt.port = [];
            opt.timeout = [];
            
            [opt, arg] = tb_optparse(opt, varargin);
            
            r = r@SerialLink(arg{:});
            
            % set properties of robot object from passed options or
            % defaults
            for p = properties(r)'
                p = p{1};   % property name
                
                if isfield(opt, p) && ~isempty( opt.(p) )
                    % if there's a set option, override what's in the robot
                    r.(p) = opt.(p);
                end
                
                if isfield(default, p) && isempty( r.(p) )
                    % otherwise if there's a set default, use that
                    r.(p) = default.(p); 
                end
            end
            
            % TCP/IP communication config
            r.tcpipClient = tcpip(r.ip, r.port, 'NetworkRole', 'Client');
            set(r.tcpipClient, 'Timeout', r.timeout);
        end
        
        function plot(r, q)
            plot@SerialLink(r, q);
            r.sendQ(q);
        end
        
        function sendQ(r, q)
            msg = ":";
            for i=1:length(q)
                msg = msg + num2str(q(i)) + ':';
            end
            r.sendMsg(msg);
        end
        
    end
    
    methods (Access = private)
        function sendMsg(r, msg)
        %UnityLink.sendMsg Send message to Unity app
            fopen(r.tcpipClient);
            fwrite(r.tcpipClient, msg);
            fclose(r.tcpipClient);
        end
    end
end