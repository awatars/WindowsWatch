--
-- PostgreSQL database dump
--

\restrict GSgieIN7ijhTm0zz92Pg0jQpl7LzPxCBU0YigJSZ43lDE2mhj2XClbq8NYVsZzA

-- Dumped from database version 18.1
-- Dumped by pg_dump version 18.0

-- Started on 2025-12-24 14:30:34

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5082 (class 1262 OID 16502)
-- Name: WatchShopDB; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "WatchShopDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';


ALTER DATABASE "WatchShopDB" OWNER TO postgres;

\unrestrict GSgieIN7ijhTm0zz92Pg0jQpl7LzPxCBU0YigJSZ43lDE2mhj2XClbq8NYVsZzA
\connect "WatchShopDB"
\restrict GSgieIN7ijhTm0zz92Pg0jQpl7LzPxCBU0YigJSZ43lDE2mhj2XClbq8NYVsZzA

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 220 (class 1259 OID 16624)
-- Name: clients; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.clients (
    id integer NOT NULL,
    full_name character varying(150) NOT NULL,
    inn character varying(20),
    address character varying(255),
    phone character varying(20),
    email character varying(100)
);


ALTER TABLE public.clients OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16623)
-- Name: clients_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.clients_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.clients_id_seq OWNER TO postgres;

--
-- TOC entry 5083 (class 0 OID 0)
-- Dependencies: 219
-- Name: clients_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.clients_id_seq OWNED BY public.clients.id;


--
-- TOC entry 224 (class 1259 OID 16647)
-- Name: components; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.components (
    id integer NOT NULL,
    category character varying(50),
    name character varying(100) NOT NULL,
    unit_cost numeric(18,2),
    unit_measure character varying(20)
);


ALTER TABLE public.components OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 16646)
-- Name: components_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.components_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.components_id_seq OWNER TO postgres;

--
-- TOC entry 5084 (class 0 OID 0)
-- Dependencies: 223
-- Name: components_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.components_id_seq OWNED BY public.components.id;


--
-- TOC entry 230 (class 1259 OID 16697)
-- Name: orderitems; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.orderitems (
    id integer NOT NULL,
    order_id integer NOT NULL,
    watch_model_id integer NOT NULL
);


ALTER TABLE public.orderitems OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 16696)
-- Name: orderitems_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.orderitems_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.orderitems_id_seq OWNER TO postgres;

--
-- TOC entry 5085 (class 0 OID 0)
-- Dependencies: 229
-- Name: orderitems_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.orderitems_id_seq OWNED BY public.orderitems.id;


--
-- TOC entry 228 (class 1259 OID 16677)
-- Name: orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.orders (
    id integer NOT NULL,
    client_id integer NOT NULL,
    watch_model_id integer,
    order_date date DEFAULT CURRENT_DATE,
    status character varying(50),
    total_amount numeric(18,2)
);


ALTER TABLE public.orders OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 16676)
-- Name: orders_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.orders_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.orders_id_seq OWNER TO postgres;

--
-- TOC entry 5086 (class 0 OID 0)
-- Dependencies: 227
-- Name: orders_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.orders_id_seq OWNED BY public.orders.id;


--
-- TOC entry 232 (class 1259 OID 16717)
-- Name: receipts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.receipts (
    id integer NOT NULL,
    order_id integer NOT NULL,
    receipt_date date DEFAULT CURRENT_DATE,
    manager character varying(100),
    payment_method character varying(50),
    total_amount numeric(18,2)
);


ALTER TABLE public.receipts OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 16716)
-- Name: receipts_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.receipts_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.receipts_id_seq OWNER TO postgres;

--
-- TOC entry 5087 (class 0 OID 0)
-- Dependencies: 231
-- Name: receipts_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.receipts_id_seq OWNED BY public.receipts.id;


--
-- TOC entry 226 (class 1259 OID 16656)
-- Name: recipes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.recipes (
    id integer NOT NULL,
    watch_model_id integer NOT NULL,
    component_id integer NOT NULL,
    quantity integer NOT NULL
);


ALTER TABLE public.recipes OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 16655)
-- Name: recipes_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.recipes_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.recipes_id_seq OWNER TO postgres;

--
-- TOC entry 5088 (class 0 OID 0)
-- Dependencies: 225
-- Name: recipes_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.recipes_id_seq OWNED BY public.recipes.id;


--
-- TOC entry 222 (class 1259 OID 16635)
-- Name: watchmodels; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.watchmodels (
    id integer NOT NULL,
    description text,
    model_name character varying(100) CONSTRAINT watchmodels_name_not_null NOT NULL,
    image_path character varying(255),
    type character varying(50),
    price numeric(18,2) NOT NULL
);


ALTER TABLE public.watchmodels OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 16634)
-- Name: watchmodels_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.watchmodels_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.watchmodels_id_seq OWNER TO postgres;

--
-- TOC entry 5089 (class 0 OID 0)
-- Dependencies: 221
-- Name: watchmodels_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.watchmodels_id_seq OWNED BY public.watchmodels.id;


--
-- TOC entry 4886 (class 2604 OID 16627)
-- Name: clients id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients ALTER COLUMN id SET DEFAULT nextval('public.clients_id_seq'::regclass);


--
-- TOC entry 4888 (class 2604 OID 16650)
-- Name: components id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.components ALTER COLUMN id SET DEFAULT nextval('public.components_id_seq'::regclass);


--
-- TOC entry 4892 (class 2604 OID 16700)
-- Name: orderitems id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orderitems ALTER COLUMN id SET DEFAULT nextval('public.orderitems_id_seq'::regclass);


--
-- TOC entry 4890 (class 2604 OID 16680)
-- Name: orders id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders ALTER COLUMN id SET DEFAULT nextval('public.orders_id_seq'::regclass);


--
-- TOC entry 4893 (class 2604 OID 16720)
-- Name: receipts id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receipts ALTER COLUMN id SET DEFAULT nextval('public.receipts_id_seq'::regclass);


--
-- TOC entry 4889 (class 2604 OID 16659)
-- Name: recipes id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recipes ALTER COLUMN id SET DEFAULT nextval('public.recipes_id_seq'::regclass);


--
-- TOC entry 4887 (class 2604 OID 16638)
-- Name: watchmodels id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.watchmodels ALTER COLUMN id SET DEFAULT nextval('public.watchmodels_id_seq'::regclass);


--
-- TOC entry 5064 (class 0 OID 16624)
-- Dependencies: 220
-- Data for Name: clients; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.clients VALUES (1, 'Иванов Иван', '1234567890', 'Москва', '89001112233', 'ivan@mail.ru');
INSERT INTO public.clients VALUES (2, '1', '1', '1', '1', '1');
INSERT INTO public.clients VALUES (3, '112', '11', '1123', '11', '11');


--
-- TOC entry 5068 (class 0 OID 16647)
-- Dependencies: 224
-- Data for Name: components; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.components VALUES (1, '123', '123', 2.00, '1234');


--
-- TOC entry 5074 (class 0 OID 16697)
-- Dependencies: 230
-- Data for Name: orderitems; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5072 (class 0 OID 16677)
-- Dependencies: 228
-- Data for Name: orders; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.orders VALUES (1, 1, 1, '2025-12-23', 'Новый', 2.00);
INSERT INTO public.orders VALUES (2, 3, 1, '2025-12-24', 'Новый', 10000.00);


--
-- TOC entry 5076 (class 0 OID 16717)
-- Dependencies: 232
-- Data for Name: receipts; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5070 (class 0 OID 16656)
-- Dependencies: 226
-- Data for Name: recipes; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5066 (class 0 OID 16635)
-- Dependencies: 222
-- Data for Name: watchmodels; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.watchmodels VALUES (1, NULL, 'Rolex Submariner', NULL, NULL, 50000.00);
INSERT INTO public.watchmodels VALUES (2, NULL, 'Casio G-Shock', NULL, NULL, 5000.00);


--
-- TOC entry 5090 (class 0 OID 0)
-- Dependencies: 219
-- Name: clients_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.clients_id_seq', 3, true);


--
-- TOC entry 5091 (class 0 OID 0)
-- Dependencies: 223
-- Name: components_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.components_id_seq', 1, true);


--
-- TOC entry 5092 (class 0 OID 0)
-- Dependencies: 229
-- Name: orderitems_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.orderitems_id_seq', 1, false);


--
-- TOC entry 5093 (class 0 OID 0)
-- Dependencies: 227
-- Name: orders_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.orders_id_seq', 2, true);


--
-- TOC entry 5094 (class 0 OID 0)
-- Dependencies: 231
-- Name: receipts_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.receipts_id_seq', 1, false);


--
-- TOC entry 5095 (class 0 OID 0)
-- Dependencies: 225
-- Name: recipes_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.recipes_id_seq', 1, false);


--
-- TOC entry 5096 (class 0 OID 0)
-- Dependencies: 221
-- Name: watchmodels_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.watchmodels_id_seq', 2, true);


--
-- TOC entry 4896 (class 2606 OID 16633)
-- Name: clients clients_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients
    ADD CONSTRAINT clients_pkey PRIMARY KEY (id);


--
-- TOC entry 4900 (class 2606 OID 16654)
-- Name: components components_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.components
    ADD CONSTRAINT components_pkey PRIMARY KEY (id);


--
-- TOC entry 4906 (class 2606 OID 16705)
-- Name: orderitems orderitems_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orderitems
    ADD CONSTRAINT orderitems_pkey PRIMARY KEY (id);


--
-- TOC entry 4904 (class 2606 OID 16685)
-- Name: orders orders_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);


--
-- TOC entry 4908 (class 2606 OID 16725)
-- Name: receipts receipts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receipts
    ADD CONSTRAINT receipts_pkey PRIMARY KEY (id);


--
-- TOC entry 4902 (class 2606 OID 16665)
-- Name: recipes recipes_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recipes
    ADD CONSTRAINT recipes_pkey PRIMARY KEY (id);


--
-- TOC entry 4898 (class 2606 OID 16645)
-- Name: watchmodels watchmodels_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.watchmodels
    ADD CONSTRAINT watchmodels_pkey PRIMARY KEY (id);


--
-- TOC entry 4913 (class 2606 OID 16706)
-- Name: orderitems orderitems_order_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orderitems
    ADD CONSTRAINT orderitems_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(id);


--
-- TOC entry 4914 (class 2606 OID 16711)
-- Name: orderitems orderitems_watch_model_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orderitems
    ADD CONSTRAINT orderitems_watch_model_id_fkey FOREIGN KEY (watch_model_id) REFERENCES public.watchmodels(id);


--
-- TOC entry 4911 (class 2606 OID 16686)
-- Name: orders orders_client_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_client_id_fkey FOREIGN KEY (client_id) REFERENCES public.clients(id);


--
-- TOC entry 4912 (class 2606 OID 16691)
-- Name: orders orders_watch_model_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_watch_model_id_fkey FOREIGN KEY (watch_model_id) REFERENCES public.watchmodels(id);


--
-- TOC entry 4915 (class 2606 OID 16726)
-- Name: receipts receipts_order_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receipts
    ADD CONSTRAINT receipts_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(id);


--
-- TOC entry 4909 (class 2606 OID 16671)
-- Name: recipes recipes_component_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recipes
    ADD CONSTRAINT recipes_component_id_fkey FOREIGN KEY (component_id) REFERENCES public.components(id);


--
-- TOC entry 4910 (class 2606 OID 16666)
-- Name: recipes recipes_watch_model_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recipes
    ADD CONSTRAINT recipes_watch_model_id_fkey FOREIGN KEY (watch_model_id) REFERENCES public.watchmodels(id);


-- Completed on 2025-12-24 14:30:35

--
-- PostgreSQL database dump complete
--

\unrestrict GSgieIN7ijhTm0zz92Pg0jQpl7LzPxCBU0YigJSZ43lDE2mhj2XClbq8NYVsZzA

